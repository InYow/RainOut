using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item_ScriptableObject", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    //FIN:物品的信息
    public Sprite icon;
    public string itemName;
    [TextArea(3, 7)]
    public string description;
    public string tag;
}