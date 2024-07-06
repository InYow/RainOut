using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Me02 : Skill
{
    public int value;
    public override void Effect_Origin()
    {
        MoCa(origin, origin, "atk", value);
    }
}
