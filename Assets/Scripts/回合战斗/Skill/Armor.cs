using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Skill
{
    [Tooltip("起甲数值")] public int value;

    public override void SetOriginAndTarget(Entity origin, Entity target)
    {
        Armor(origin, target, value);
    }
}
