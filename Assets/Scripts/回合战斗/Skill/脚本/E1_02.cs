using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_02 : Skill
{
    public override void Effect()
    {
        Armor(origin, origin, 5);
        Attack(origin, target, 50);

        AudioPlay();
    }
}
