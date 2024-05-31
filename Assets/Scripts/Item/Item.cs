using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    //TODO: 优化,可堆叠物品
    public ItemData data;
    public string LayerName;
    public int LayerID;
    public ItemTextUI itemTextUI;
    public bool Carrying;
    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        LayerName = spriteRenderer.sortingLayerName;
        LayerID = spriteRenderer.sortingOrder;
        itemTextUI = GetComponent<ItemTextUI>(); itemTextUI?.SetText(data);
    }
    public void SeeText(bool view)
    {
        if (itemTextUI == null)
        {
            itemTextUI = GetComponent<ItemTextUI>();
            itemTextUI?.SetText(data);
        }
        itemTextUI.SeeText(view);
    }
    //在手上使用时
    public virtual void Use()
    {

    }
    //被手持时
    public virtual void OnCarryOn(Hand hand)
    {
        Carrying = true;
        Debug.Log($"{gameObject.name}拿在手中");
    }
    public virtual void OnDisCarry(Hand hand)
    {
        Carrying = false;
        #region 清空手持的物体
        this.transform.parent = hand.CarryBagBlock.transform;
        this.transform.localPosition = Vector3.zero;
        this.gameObject.SetActive(false);
        #endregion
        Debug.Log($"{gameObject.name}离开了手");
    }
}
