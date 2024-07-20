using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZiRanDiuShi : State
{
    public override void OnSideOur(Entity entity)
    {
        //TODO: 自然回合丢失，应当是，获得回合前触发（换边该生成回合时不生成），而不是获得回合后触发的状态。
        RoundManager.RoundRemoveList(entity);

        DetectStackCount();
    }
}
