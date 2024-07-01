using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Intention", menuName = "ScriptableObjects/Intention/Intention", order = 1)]
public class Intention : ScriptableObject
{
    //使用的技能
    public Skill skill;

    //意图的icon
    public Sprite sprite;

    //意图的描述
    [TextArea(3, 10)] public string Des;
}
