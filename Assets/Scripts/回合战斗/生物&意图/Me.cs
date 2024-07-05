using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Me : Entity
{

    public override void Dead()
    {
        dead = true;

        RoundManager.BattleFailure();
    }
}
