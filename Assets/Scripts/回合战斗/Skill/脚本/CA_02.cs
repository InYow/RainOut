using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//透支
public class CA_02 : Skill
{
    [Header("自定义")]

    public ZiRanDiuShi state;

    public override void Effect_Origin()
    {
        MoCa(origin, origin, "atk", 40);
        MoCa(origin, origin, "def", 40);
        MoCa(origin, origin, "spd", 40);

        origin.StateAdd(state, 1);
    }
}
