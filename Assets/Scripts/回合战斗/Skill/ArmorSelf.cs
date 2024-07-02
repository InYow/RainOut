using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSelf : Skill
{
    [Tooltip("起甲数值")] public int value;

    public override bool SetOrigin(Entity origin)
    {
        Armor(origin, origin, value);
        return true;
    }

    public override void SetOriginAndTarget(Entity origin, Entity target)
    {
    }
}
