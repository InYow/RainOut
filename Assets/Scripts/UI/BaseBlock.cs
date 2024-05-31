using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public abstract class BaseBlock : MonoBehaviour
{
    //F鼠标悬浮有颜色的变化
    [HideInInspector]
    public BagBlockData data;
    [Tooltip("背包格子的图片")] public Image m_blockImage;
    public Image m_itemIcon;
    protected Sprite m_iconNone;
    public Color _HoverColor;
    public Color _NormalColor;
    protected bool shouldFalse;
    protected virtual void Init()
    {
        //TODO 改为手动指定组件
        m_blockImage = GetComponent<Image>();
        m_itemIcon = GetComponentsInChildren<Image>()[1];
        m_iconNone = m_itemIcon.sprite;
    }
    protected virtual void Update()
    {

    }
    public virtual void OnPointerEnter()
    {
        m_blockImage.color = _HoverColor;
    }
    public virtual void OnPointerExit()
    {
        m_blockImage.color = _NormalColor;
    }
    public virtual void OnClick()
    {
        //Debug.Log($"{gameObject.name}被点击了Click()");
    }
    public virtual void Push(Item item)
    {

    }
    public virtual void Pop()
    {

    }
    public virtual void Change(BaseBlock baseBlock)
    {

    }
    public virtual Item Get()
    {
        return null;
    }
    public virtual bool Empty()
    {
        return true;
    }
}
