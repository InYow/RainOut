using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//作为场景中的农作物植株
//使用方法 Instantiate Prb, Init赋值;Grow()生长 
public class CropPlant : MonoBehaviour
{
    public CropData cropData;
    public SpriteRenderer spriteRenderer;
    public int currentStage;
    public int CurrentStage
    {
        get
        {
            return currentStage;
        }
        set
        {
            currentStage = value;
            spriteRenderer.sprite = cropData.GetStage(CurrentStage).sprite;
        }
    }
    private void Start()
    {
        CurrentStage = CurrentStage;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Grow();
        }
    }
    public void Init(CropData c)
    {
        cropData = c;
        CurrentStage = 0;
    }
    public void Grow()
    {
        if (CurrentStage == cropData.StageDatas.Count - 1)
        {
            //已经成熟了
            return;
        }
        CurrentStage++;
    }
}
