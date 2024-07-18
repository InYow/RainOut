using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XuRuo : State
{
    public override void OnSideOur(Entity entity)
    {
        entity.Atk_Moca -= 0.1f;

        DetectStackCount();
    }
}
