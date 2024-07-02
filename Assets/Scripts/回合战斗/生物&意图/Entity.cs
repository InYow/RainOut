using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Tooltip("死没死")] public bool dead = false;

    [Tooltip("名称")] public string entityName;

    [Tooltip("最大生命")] public int hpMax;

    [Tooltip("生命")] public int hp;

    public int Armor //护甲
    {
        get
        {
            return entityInfoManager.entityArmor.Armor;
        }
        set
        {
            entityInfoManager.entityArmor.Armor = value;
        }
    }

    [Tooltip("攻击力")] public int atk;

    [Header("非手动赋值")]
    [Tooltip("对应圆圈")] public SelectEntity selectEntity;

    [Tooltip("状态信息")] public EntityInfoManager entityInfoManager;

    private void OnValidate()
    {
        entityInfoManager = GetComponentInChildren<EntityInfoManager>();
    }

    //受到攻击
    public void OnAttack(int atkValue)
    {
        //实际效力攻击伤害
        int validAtk = atkValue;
        if (validAtk < Armor)
        {
            int RestArmor = Armor - validAtk;
            Armor = RestArmor;
        }
        else if (validAtk == Armor)
        {
            int RestArmor = Armor - validAtk;
            Armor = RestArmor;
        }
        else if (validAtk > Armor)
        {
            int RestArmor = 0;
            int RestvalidATK = validAtk - Armor;

            Armor = RestArmor;
            HP_Detect(RestvalidATK);
        }
    }

    //减少生命值 hp
    public void HP_Detect(int value)
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
