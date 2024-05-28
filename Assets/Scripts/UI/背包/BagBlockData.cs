using UnityEngine;

[CreateAssetMenu(fileName = "BagBlock Data", menuName = "ScriptableObjects/BagBlock Data", order = 2)]
public class BagBlockData : ScriptableObject
{
    [Header("和交互相关")]
    public Color hangOnColor;
    [Header("和图层相关")]
    public string LayerName;
    public int LayerID;//
}
