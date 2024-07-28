using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//F绑定背包格子，背包值改变并改变图案，这个的图案也改变
//F左键单击切换手持,用颜色标记出当前手持的格子
public class TaskBlock : BaseBlock, IPointerClickHandler
{
    public BagBlock linkBagBlock;//链接的背包格子
    public Action OnLinkBagBlockItemAction;//LinkBagBlock为item赋值时
    public TaskBar taskBar;//this.爹
    public Color _SelectColor;
    public bool selected;
    public bool hovered;
    public bool Selected
    {
        get
        {
            return this.selected;
        }
        set
        {
            this.selected = value;
            OnSelected();
        }
    }
    private void Awake()
    {
        hovered = false;
        Selected = false;
        OnLinkBagBlockItemAction += OnLinkBagBlockItem;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        linkBagBlock.LinkTaskBlocks.Add(this);
    }
    public void OnLinkBagBlockItem()
    {
        //如果自身格子是手上的格子，并且格子的值变化了，则重新附给手上的值
        if (this.Selected)
        {
            //Debug.Log("OnLinkBagBlockItem() Taskblock");
            this.taskBar.SelectedBlock = this;
        }
    }
    //被选中时触发
    public void OnSelected()
    {
        if (Selected)
        {
            m_blockImage.color = _SelectColor;
        }
        else
        {
            if (!hovered)
            {
                m_blockImage.color = _NormalColor;
            }
            else
            {
                m_blockImage.color = _HoverColor;
            }
        }
    }
    public override void OnPointerEnter()
    {
        if (Selected)
        {

        }
        else
        {
            base.OnPointerEnter();
        }
        hovered = true;
    }
    public override void OnPointerExit()
    {
        if (Selected)
        {

        }
        else
        {
            base.OnPointerExit();
        }
        hovered = false;
    }
    public override void OnClick()
    {
        taskBar.SelectedBlock = this;
        base.OnClick();
    }
    private void OnDisable()
    {
        linkBagBlock.LinkTaskBlocks.Remove(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }
}
