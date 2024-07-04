using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Skill
{
    public override void Effect()
    {
        Attack(origin, target);
    }

}