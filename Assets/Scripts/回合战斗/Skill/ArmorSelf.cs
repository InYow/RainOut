using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSelf : Skill
{
    [Tooltip("起甲数值")] public int value;

    public override void Effect_Origin()
    {
        Armor(origin, origin, value);
    }
}
