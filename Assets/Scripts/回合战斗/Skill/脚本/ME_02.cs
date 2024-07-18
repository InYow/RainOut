using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ME_02 : Skill
{
    //残忍
    public override void Effect_Origin()
    {
        MoCa(origin, origin, "atk", 40);

        AudioPlay();
    }
}
