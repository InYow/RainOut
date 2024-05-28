using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_Weapon : InterActable
{
    private SpriteRenderer spriteRenderer;
    [Header("拾取后的图层")]
    public string picked_LayerName;
    public int picked_LayerID;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override Intertype Active()
    {
        spriteRenderer.sortingLayerName = picked_LayerName;
        spriteRenderer.sortingOrder = picked_LayerID;
        return Intertype.Pickable_Weapon;
    }
    public override void CheckStay()
    {
        Item item = GetComponent<Item>();
        item.SeeText(true);
        base.CheckStay();
    }
}
