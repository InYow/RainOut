using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ME_04 : Skill
{
    public override void Effect()
    {
        Attack(origin, target, 50);

        EnemyBrain enemyBrain = target.GetComponent<EnemyBrain>();
        var Intentions = enemyBrain.Read();

        if (Intentions[0].tagList.Contains(Tag.defend))
        {
            enemyBrain.Break(0);
            More(origin, origin);
        }

    }
}
