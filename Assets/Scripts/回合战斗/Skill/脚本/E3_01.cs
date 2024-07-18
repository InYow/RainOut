using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_01 : Skill
{
    public override void Effect()
    {
        Attack(origin, target, 100);

        AudioPlay();
    }
}
