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

    //使用技能
    /// <summary>
    /// 在这个方法中组合仅需要施法者的技能效果
    /// </summary>
    /// <param name="origin"></param>
    /// <returns>返回true 意味着结束回合</returns>
    public virtual bool SetOrigin(Entity origin)
    {
        return false;
    }

    //使用技能
    /// <summary>
    /// 在这个方法中组合技能效果
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="target"></param>
    public abstract void SetOriginAndTarget(Entity origin, Entity target);

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
