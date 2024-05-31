using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropSeed : Item
{
    public SelectTileBox selectTileBoxPrb;
    public SelectTileBox selectTileBox;
    private CropData cropData;//
    private void Awake()
    {
        if (data is CropData cropData)
        {
            this.cropData = cropData;
        }
        else
        {
            Debug.LogError($"{gameObject.name}'s type of CropSeed.Data is not Typeof(CropData)");
        }
    }
    public override void Use()
    {
        base.Use();
        //选择框指向了土地且能种植
        HoeDirt hoeDirt = selectTileBox.hoeDirt;
        if (hoeDirt != null && hoeDirt.CanPlant())
        {
            CropPlant cropPlant = Instantiate(cropData.cropPlantPrb);
            cropPlant.Init(cropData, hoeDirt);
        }
    }
    public override void OnCarryOn(Hand hand)
    {
        base.OnCarryOn(hand);
        selectTileBox = Instantiate(selectTileBoxPrb);
    }
    public override void OnDisCarry(Hand hand)
    {
        base.OnDisCarry(hand);
        Destroy(selectTileBox.gameObject);
        selectTileBox = null;
    }
}
