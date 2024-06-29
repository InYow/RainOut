using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Tooltip("名称")] public string entityName;

    [Tooltip("最大生命")] public int hpMax;

    [Tooltip("生命")] public int hp;

    [Tooltip("死没死")] public bool dead = false;

    [Tooltip("攻击力")] public int atk;

    [Header("非手动赋值")]
    [Tooltip("对应圆圈")] public SelectEntity selectEntity;

    //减少生命值 hp
    public void DetectHP(int value)
    {
        hp -= value;

        //是否战败
        if (hp <= 0)
        {
            dead = true;
            Debug.Log($"{entityName}战败");
        }
    }
}
