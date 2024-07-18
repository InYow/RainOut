using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_02 : Skill
{
    public override void Effect()
    {
        Attack(origin, target, 50);
        MoCa(origin, target, "atk", -25);

        AudioPlay();
    }
}
