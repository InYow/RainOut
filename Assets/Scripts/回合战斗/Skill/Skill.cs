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
        multiple
    }

    //目标类型
    public TargetType targetType;

    //在这个方法中组合技能效果
    //使用技能
    public abstract void SetOriginAndTarget(Entity origin, Entity target);

    //在这个方法中书写判断式
    //目标条件
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

}
