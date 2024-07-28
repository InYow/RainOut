using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_03 : Skill
{
    public override void Effect_Origin()
    {
        Armor(origin, origin, 5);
        MoCa(origin, origin, "atk", 50);

        AudioPlay();
    }
}
