using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    //回合的行动者
    public Entity master;

    //第几个回合
    public int id;

    public static Round Create(Round roundPrb, Transform trans, Entity entity, int i)
    {
        Round round = Instantiate(roundPrb, trans);
        round.gameObject.name = entity.entityName;
        round.master = entity;
        round.id = i;

        return round;
    }
}
