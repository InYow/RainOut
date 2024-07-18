using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_03 : Skill
{
    public override void Effect()
    {
        Attack(origin, target, 50);

        AudioPlay();
    }
}
