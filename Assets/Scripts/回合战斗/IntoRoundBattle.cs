using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntoRoundBattle : MonoBehaviour
{
    public List<Entity> AList;

    public List<Entity> BList;

    void Start()
    {
        foreach (var entity in AList)
        {
            RoundManager.AddToAList(entity);
        }

        foreach (var entity in BList)
        {
            RoundManager.AddToBList(entity);
        }

        RoundManager.RoundStart(1);

    }
}
