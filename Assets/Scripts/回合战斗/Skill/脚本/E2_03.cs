using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_03 : Skill
{
    [Header("自定义")]

    public QiLing state;

    public override void Effect()
    {
        Attack(origin, target, 50);
        target.StateAdd(state, 2);

        AudioPlay();
    }
}
