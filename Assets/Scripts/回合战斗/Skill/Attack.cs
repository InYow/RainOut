using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Skill
{
    public override void SetOriginAndTarget(Entity origin, Entity target)
    {
        int hurtvalue = (int)origin.atk;
        target.DetectHP(hurtvalue);
    }
}
