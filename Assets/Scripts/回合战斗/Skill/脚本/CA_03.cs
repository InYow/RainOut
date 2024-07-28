using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CA_03 : Skill
{
    public override void Effect()
    {
        Attack(origin, target, 25);

        RoundInfo roundInfo = RoundManager.HistoryRead(0);
        if (roundInfo != null
        && roundInfo?.origin?.entityName == "Me"
        && roundInfo?.target == target)
        {
            MoCa(origin, target, "def", -50);
        }

        AudioPlay();

    }
}
