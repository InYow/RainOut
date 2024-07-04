using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [Tooltip("释放者")] public Entity origin;

    [Tooltip("目标")] public Entity target;

    [Header("技能信息")]
    [Tooltip("名称")] public string skillName;

    [Tooltip("描述")] public string skillDes;

    [Tooltip("动画状态名")] public string stateName;

    //施法目标类型
    public enum TargetType
    {
        single = 0,
        multiple,
        self
    }

    //目标类型
    public TargetType targetType;

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
    /// <param name="origin"></param>
    /// <param name="target"></param>
    public void Attack(Entity origin, Entity target)
    {
        int hurtvalue = (int)origin.atk;
        target.OnAttack(hurtvalue);
    }

    /// <summary>
    /// 一个生物为一个生物起甲
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    public void Armor(Entity origin, Entity target, int value)
    {
        target.Armor = value;
    }
}
