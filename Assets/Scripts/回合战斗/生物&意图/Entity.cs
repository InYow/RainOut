using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("组件")]

    [Tooltip("生命条")] public HealthBar healthBar;

    [Tooltip("护甲块")] public ArmorBar entityArmor;

    [Tooltip("动画机")] public Animator animator;

    [Tooltip("属性位置")] public Transform moca_Trans;

    [Tooltip("实例列表")] public List<UpDownChecker> MoCaList;//生命、攻击、防御、速度

    [Tooltip("被击中音效")] public AudioSource AudioSourceOnAttack;

    [Header("信息")]

    [Tooltip("死没死")] public bool dead = false;

    [Tooltip("名称")] public string entityName;

    [Tooltip("最大生命")] public int hpMax;

    [Tooltip("生命")] public int hp;

    [Tooltip("护甲")] public int armor;

    [Tooltip("攻击力")] public int atk;

    [Tooltip("防御力")] public int def;

    [Tooltip("敏捷值")] public int spd;

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

    public float Atk
    {
        get
        {
            float n = atk * Atk_Moca;

            return n;
        }
    }

    public float Def
    {
        get
        {
            float n = def * Def_Moca;

            return n;
        }
    }

    public float Spd
    {
        get
        {
            float n = spd * Spd_Moca;

            return n;
        }
    }


    [Header("状态")]

    public StateManager stateManager;










    public void Start()
    {
        StartInit();
    }

    public void StartInit()
    {
        healthBar?.ShowHealth(Hp, hpMax);
        entityArmor?.ShowArmor(Armor);
    }

    private void OnValidate()
    {
        AudioSourceOnAttack = GetComponent<AudioSource>();
        if (AudioSourceOnAttack != null)
        {
            AudioSourceOnAttack.playOnAwake = false;
        }

        animator = GetComponent<Animator>();

        stateManager = GetComponent<StateManager>();
    }
    /// <summary>
    /// 动画触发技能效果
    /// </summary>
    public void AnimaSkill()
    {
        RoundManager.AnimaSkillEffect();
    }

    //到我们是行动方了
    public void SideOur()
    {
        //调整属性
        if (HP_Moca != 1f)
        {
            if (Mathf.Abs(HP_Moca - 1f) <= 0.05f)
            {
                HP_Moca = 1f;
            }
            else
            {
                HP_Moca += Mathf.Sign(1f - HP_Moca) * 0.05f;
            }
        }
        if (Atk_Moca != 1f)
        {
            // if (stateManager != null /*&& stateManager?.FindWithClassName("XuRuo") == null*/)
            // {
            if (Mathf.Abs(Atk_Moca - 1f) <= 0.05f)
            {
                Atk_Moca = 1f;
            }
            else
            {
                Atk_Moca += Mathf.Sign(1f - Atk_Moca) * 0.05f;
            }
            //}
        }
        if (Def_Moca != 1f)
        {
            if (Mathf.Abs(Def_Moca - 1f) <= 0.05f)
            {
                Def_Moca = 1f;
            }
            else
            {
                Def_Moca += Mathf.Sign(1f - Def_Moca) * 0.05f;
            }
        }
        if (Spd_Moca != 1f)
        {
            if (Mathf.Abs(Spd_Moca - 1f) <= 0.05f)
            {
                Spd_Moca = 1f;
            }
            else
            {
                Spd_Moca += Mathf.Sign(1f - Spd_Moca) * 0.05f;
            }
        }

        //触发状态
        stateManager?.SideOur(this);

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
    public void OnAttack(float atkValue/*已加上技能威力*/, Skill skill)
    {
        stateManager.AttackGet(this, ref atkValue, skill);

        //受到攻击伤害应当扣除的血量 = 受到的攻击伤害 - 自身防御力
        float differValue = atkValue - this.Def;

        //实际效力攻击伤害/受到攻击伤害应当扣除的血量
        int validValue;

        if (differValue >= 0f && differValue < 0.1f)
        {
            validValue = 0;
        }
        else if (differValue >= 0.1f && differValue <= 1f)
        {
            validValue = 1;
        }
        else
        {
            validValue = (int)differValue;
        }

        //先尝试扣除护甲，然后再扣除生命值
        if (validValue < Armor)
        {
            int RestArmor = Armor - validValue;
            Armor = RestArmor;
        }
        else if (validValue == Armor)
        {
            int RestArmor = Armor - validValue;
            Armor = RestArmor;
        }
        else if (validValue > Armor)
        {
            int RestArmor = 0;
            int RestvalidATK = validValue - Armor;

            Armor = RestArmor;
            HP_Detect(RestvalidATK);
        }

        animator.Play("受击", 0, 0f);
        AudioSourceOnAttack.Play();
    }

    //减少生命值 hp
    public void HP_Detect(int value)
    {
        Hp -= value;

        HurtValueFlow hurtValueFlow = Instantiate(Resources.Load<HurtValueFlow>("Prefabs/伤害显示"), transform.parent);
        hurtValueFlow.SetText($"{value}");

        //是否战败
        if (Hp <= 0)
        {
            Dead();
        }
    }

    //被施加状态
    public void StateAdd(State s, int stack)
    {
        stateManager.StateAdd(s, stack);
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
