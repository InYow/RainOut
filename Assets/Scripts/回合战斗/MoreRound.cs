using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreRound : Round
{
    public static MoreRound Create(MoreRound roundPrb, Transform trans, Entity entity, int i)
    {
        MoreRound round = Instantiate(roundPrb, trans);
        round.gameObject.name = entity.entityName;
        round.master = entity;
        round.id = i;

        return round;
    }
}
