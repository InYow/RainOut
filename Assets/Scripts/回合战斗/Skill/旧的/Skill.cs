using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Effect() Effect_Origin() 空定义
/// </summary>
public abstract class Skill : MonoBehaviour
{
    [Tooltip("释放者")] public Entity origin;

    [Tooltip("目标")] public Entity target;

    [Header("技能信息")]

    [Tooltip("名称")] public string skillName;

    [Tooltip("描述")] public string skillDes;

    [Header("动画播放")]

    [Tooltip("动画状态名")] public string stateName;

    [Header("音频")]

    [Tooltip("脚本同物体的AudioSource")] public AudioSource audioSource;

    //施法目标类型
    public enum TargetType
    {
        single = 0,
        multiple,
        self
    }

    //目标类型
    public TargetType targetType;

    private void OnValidate()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.playOnAwake = false;
        else
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    /// <summary>
    /// 在这个方法中书写判断式, 目标条件
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual bool IfCanTarget(Entity entity)
    {
        if (!entity.dead)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region 不需要指定目标的技能
    /// <summary>
    /// 在这个方法中组合仅需要施法者的技能效果
    /// </summary>
    /// <param name="origin"></param>
    /// <returns>返回true 意味着结束回合</returns>
    public virtual void SetOrigin(Entity origin)
    {
        //判断是否触发
        if (targetType == TargetType.self)
        {
            //关闭展开的UI
            UIManager.ClearList();

            //圆圈关闭
            RoundManager.SelectAllSet(false);

            this.origin = origin;

            //播放动画
            AnimaPlay();

            //(接下面)
        }
    }

    public virtual void SetOriginBehind()
    {
        //(接上面)

        //技能效果
        Effect_Origin();
    }

    /// <summary>
    /// 在这个方法中组合技能效果_Origin, SKill中空定义
    /// </summary>
    public virtual void Effect_Origin()
    {

    }

    #endregion

    #region 指定目标的技能
    /// <summary>
    /// 使用技能, 有 AnimaPlay() Effect()
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    public virtual void SetOriginAndTarget(Entity origin, Entity target)
    {
        //关闭展开的UI
        UIManager.ClearList();

        //圆圈关闭
        RoundManager.SelectAllSet(false);

        this.origin = origin;
        this.target = target;

        //播放动画
        AnimaPlay();

        //(接下面)
    }

    public virtual void SetOriginAndTargetBehind()
    {
        //(接上面)

        //技能效果
        Effect();
    }

    /// <summary>
    /// 在这个方法中组合技能效果, SKill中空定义
    /// </summary>
    public virtual void Effect()
    {

    }

    #endregion

    /// <summary>
    /// 播放动画
    /// </summary>
    public virtual void AnimaPlay()
    {
        origin.animator.Play(stateName);
    }

    //播放声音AudioSource
    public virtual void AudioPlay()
    {
        audioSource.Play();
    }

    //动画 触发技能效果 事件
    public virtual void AnimaSkillEffect()
    {
        if (targetType == TargetType.self)
        {
            SetOriginBehind();
        }
        else
        {
            SetOriginAndTargetBehind();
        }
    }

    //-----------------------------------------------------------------------------------
    //技能效果
    //-----------------------------------------------------------------------------------

    /// <summary>
    /// 一个生物攻击另一个生物
    /// </summary>
    public void Attack(Entity origin, Entity target, int percent)
    {
        float hurtvalue = origin.Atk * (percent / 100f);//技能威力

        target.OnAttack(hurtvalue, this);

    }

    /// <summary>
    /// 一个生物为一个生物起甲
    /// </summary>
    public void Armor(Entity origin, Entity target, int value)
    {
        target.Armor = value;
    }

    /// <summary>
    /// 一个生物修正另一个生物的属性
    /// </summary>
    /// <param name="atb">hp atk def spd</param>
    /// <param name="value">50->提升百分之50</param>
    public static void MoCa(Entity origin, Entity target, string atb, int n)
    {
        float value = n / 100f + 1;
        if (atb == "hp")
        {
            target.HP_Moca = value;
        }
        else if (atb == "atk")
        {
            target.Atk_Moca = value;
        }
        else if (atb == "def")
        {
            target.Def_Moca = value;
        }
        else if (atb == "spd")
        {
            target.Spd_Moca = value;
        }
        else
        {
            throw new NotImplementedException("打错字了");
        }
    }

    /// <summary>
    /// 一个生物令另一个生物获得额外回合
    /// </summary>
    public void More(Entity origin, Entity target)
    {
        RoundManager.MoreAdd(origin);
    }
}
