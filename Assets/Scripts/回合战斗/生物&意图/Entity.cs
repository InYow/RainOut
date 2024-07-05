using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("组件")]

    [Tooltip("生命条")] public HealthBar healthBar;

    [Tooltip("护甲块")] public ArmorBar entityArmor;

    [Tooltip("动画机")] public Animator animator;


    [Header("信息")]

    [Tooltip("死没死")] public bool dead = false;

    [Tooltip("名称")] public string entityName;

    [Tooltip("最大生命")] public int hpMax;

    [Tooltip("生命")] public int hp;


    [Tooltip("生命")]
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            healthBar?.ShowHealth(Hp, hpMax);
        }
    }

    [Tooltip("护甲")] public int armor;


    [Tooltip("护甲")]
    public int Armor
    {
        get
        {
            return armor;
        }
        set
        {
            armor = value;
            entityArmor?.ShowArmor(Armor);
        }
    }

    [Tooltip("攻击力")] public int atk;


    [Header("非手动赋值")]

    [Tooltip("对应圆圈")] public SelectEntity selectEntity;

    public void Start()
    {
        StartInit();
    }

    public void StartInit()
    {
        healthBar?.ShowHealth(Hp, hpMax);
        entityArmor?.ShowArmor(Armor);
    }

    /// <summary>
    /// 动画触发技能效果
    /// </summary>
    public void AnimaSkill()
    {
        RoundManager.AnimaSkillEffect();
    }

    //--------------------------------------------------------------------

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

        animator.Play("受击", 0, 0f);
    }

    //减少生命值 hp
    public void HP_Detect(int value)
    {
        Hp -= value;

        //是否战败
        if (Hp <= 0)
        {
            Dead();
        }
    }

    public virtual void Dead()
    {
        dead = true;
        //Debug.Log($"{entityName}战败");

        RoundManager.BattleEnd();
    }
}
