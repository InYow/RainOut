using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_02 : Skill
{
    public override void Effect()
    {
        Attack(origin, target, 50);
        MoCa(origin, target, "def", -25);

        AudioPlay();
    }
}
