using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropSeed : Item
{
    public SelectTileBox selectTileBoxPrb;
    public SelectTileBox selectTileBox;
    public override void Use()
    {
        base.Use();
    }
    public override void OnCarryOn(Hand hand)
    {
        base.OnCarryOn(hand);
        selectTileBox = Instantiate(selectTileBoxPrb);
    }
    public override void OnDisCarry(Hand hand)
    {
        base.OnDisCarry(hand);
        //Destroy(selectTileBox.gameObject);
        //selectTileBox = null;
    }
}
