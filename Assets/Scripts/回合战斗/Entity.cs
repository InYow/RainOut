using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Tooltip("名称")] public string entityName;

    [Tooltip("最大生命")] public int hpMax;

    [Tooltip("生命")] public int hp;

    [Tooltip("攻击力")] public int atk;

    //减少生命值 hp
    public void DetectHP(int value)
    {
        hp -= value;

        //是否战败
        if (hp <= 0)
        {
            Debug.Log($"{entityName}战败");
        }
    }
}
