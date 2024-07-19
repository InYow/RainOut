using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_03 : Skill
{
    [Header("自定义")]
    public State XuRuo;

    public override void Effect()
    {
        Attack(origin, target, 50);
        target.StateAdd(XuRuo, 2);

        AudioPlay();
    }
}
