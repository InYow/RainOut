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

    [Tooltip("属性位置")] public Transform moca_Trans;

    [Tooltip("实例列表")] public List<UpDownChecker> MoCaList;//生命、攻击、防御、速度

    [Header("信息")]

    [Tooltip("死没死")] public bool dead = false;

    [Tooltip("名称")] public string entityName;

    [Tooltip("最大生命")] public int hpMax;

    [Tooltip("生命")] public int hp;

    [Tooltip("护甲")] public int armor;

    [Tooltip("攻击力")] public int atk;

    [Header("属性修正")]

    public float hp_Moca;

    public float atk_Moca;

    public float def_Moca;

    public float spd_Moca;

    [Header("非手动赋值")]

    [Tooltip("对应圆圈")] public SelectEntity selectEntity;






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

    public int Atk
    {
        get
        {
            int n = (int)(atk * Atk_Moca);
            return n;
        }
    }






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

    //属性修正
    public float HP_Moca
    {
        get
        {
            return hp_Moca;
        }
        set
        {
            hp_Moca = value;

            MoCaList[0].SetInfo(value);
        }
    }
    public float Atk_Moca//属性修正
    {
        get
        {
            return atk_Moca;
        }
        set
        {
            atk_Moca = value;

            MoCaList[1].SetInfo(value);
        }
    }
    public float Def_Moca//属性修正
    {
        get
        {
            return def_Moca;
        }
        set
        {
            def_Moca = value;

            MoCaList[2].SetInfo(value);
        }
    }
    public float Spd_Moca//属性修正
    {
        get
        {
            return spd_Moca;
        }
        set
        {
            spd_Moca = value;

            MoCaList[3].SetInfo(value);
        }
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

    //生物死亡
    public virtual void Dead()
    {
        dead = true;

        //尝试结束战斗
        RoundManager.BattleEnd();
    }

    //回合交替
    public virtual void Change()
    {
        //击手者，自己是接替者
        Entity e_before = RoundManager.roundManager.originEntity;

        RoundManager.MoreChange(this);
    }
}
