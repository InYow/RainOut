using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New CropData", menuName = "ScriptableObjects/Crop/CropData", order = 1)]
public class CropData : ItemData
{
    [Serializable]
    public class CropStageData
    {
        [Tooltip("阶段图片")] public Sprite sprite;
        [Tooltip("阶段天数")] public int days;
    }
    public List<CropStageData> StageDatas;
    public CropStageData GetStage(int index)
    {
        return StageDatas[index];
    }
}
