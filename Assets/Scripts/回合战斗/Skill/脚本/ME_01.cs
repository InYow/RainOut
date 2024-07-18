using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ME_01 : Skill
{
    //击打

    public override void Effect()
    {
        Attack(origin, target, 50);

        AudioPlay();
    }
}
