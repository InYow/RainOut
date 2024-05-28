using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    //FIN:优化,可堆叠物品
    public ItemScriptableObject data;
    public string LayerName;
    public int LayerID;
    public ItemTextUI itemTextUI;
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
    public virtual void Use()
    {

    }
    //手持
    public virtual void OnCarryOn(Hand hand)
    {
        Debug.Log($"{gameObject.name}被拾起来了");
    }
}
