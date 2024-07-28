using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiLing : State
{
    public override void OnAttackGet(Entity entity, ref float minuend, Skill skill)
    {
        if (skill.targetType == Skill.TargetType.self || skill.targetType == Skill.TargetType.single)
        {
            minuend *= 1.5f;
        }
        else if (skill.targetType == Skill.TargetType.multiple)
        {
            minuend *= 0.5f;
        }

        DetectStackCount();
    }

    public override void OnSideOur(Entity entity)
    {
        DetectStackCount();
    }
}
