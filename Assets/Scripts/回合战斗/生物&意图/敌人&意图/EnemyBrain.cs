using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyBrain : MonoBehaviour
{
    [Tooltip("生物体")] public Entity entity;

    [Tooltip("脑子模板")] public BrainTable brainTable;

    [Tooltip("行动意图列表")] public List<Intention> IntentionList;

    [Tooltip("旗下检视管理者")] public IntentionCheckerManager intentionCheckerManager;

    private void OnValidate()
    {
        if (entity == null)
        {
            entity = GetComponent<Entity>();
        }
    }

    [ContextMenu("动态生成意图")]
    public void UpToNumber()
    {

        //观察到的意图数量
        int number = intentionCheckerManager.intentionCheckerList.Count;

        //动态生成行动意图
        for (; IntentionList.Count < number;)
        {
            Intention intention = brainTable.RandomIntention();
            IntentionList.Add(intention);
        }

    }

    [ContextMenu("刷新行动意图显示")]
    public void Refresh()
    {
        List<IntentionChecker> checkerList = intentionCheckerManager.intentionCheckerList;
        int i = 0;
        foreach (var checker in checkerList)
        {
            checker.SetIntention(IntentionList[i]);
            i++;
        }
    }
}
