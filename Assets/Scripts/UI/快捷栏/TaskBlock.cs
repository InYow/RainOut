using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//F绑定背包格子，背包值改变并改变图案，这个的图案也改变
//F左键单击切换手持,用颜色标记出当前手持的格子
public class TaskBlock : BaseBlock, IPointerClickHandler
{
    public BagBlock signedBagBlock;//链接的背包格子
    public TaskBar taskBar;//this.爹
    public Color _SelectColor;
    private bool selected;
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
            OnSelectedTaskBlock();
        }
    }
    public Action OnSignedBagBlockItemChangeAction;//绑定的BagBlock的item改变时调用
    private void Awake()
    {
        hovered = false;
        Selected = false;
        OnSignedBagBlockItemChangeAction += OnSignedBagBlockItemChange;
    }
    private void OnSignedBagBlockItemChange()
    {
        if (Selected)
        {
            taskBar.SelectedBlock = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        signedBagBlock.SignedTaskBlocks.Add(this);
    }
    //被选中时触发
    public void OnSelectedTaskBlock()
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
        Debug.Log("what");
        taskBar.SelectedBlock = this;
        base.OnClick();
    }
    // public override void OnClick(RightClickBag rightClickBag)
    // {
    //     base.OnClick(rightClickBag);
    //     //FIN 实时找到上层
    //     //FIN 向上层更新状态
    //     //FIN 上层更新的同时做出动作
    //     TaskBar taskBar = GetComponentInParent<TaskBar>();
    //     taskBar.Select(this);
    // }
    private void OnDisable()
    {
        signedBagBlock.SignedTaskBlocks.Remove(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }
}
