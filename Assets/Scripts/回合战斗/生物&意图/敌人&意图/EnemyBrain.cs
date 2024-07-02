using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyBrain : MonoBehaviour
{
    [Tooltip("生物体")] public Entity entity;

    [Tooltip("脑子模板")] public BrainTable brainTable;

    [Tooltip("行动意图列表")] public List<Intention> IntentionList;

    [Tooltip("意图检视管理者")] public IntentionCheckerManager intentionCheckerManager;

    private void OnValidate()
    {
        if (entity == null)
        {
            entity = GetComponent<Entity>();
        }
    }

    private void Start()
    {
        UpToNumber();
        Refresh();
    }

    [ContextMenu("动态生成意图")]
    public void UpToNumber()
    {
        intentionCheckerManager.UpToNumber();

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

    /// <summary>
    /// 在这里调用索敌行为规则
    /// </summary>
    /// <returns></returns>
    public Entity FindTarget()
    {
        //获取对面队伍
        List<Entity> AList = RoundManager.GetAList();

        //获取目标
        Entity target = null;
        //第一个没死的
        foreach (var e in AList)
        {
            if (!e.dead)
            {
                target = e;
                break;
            }
        }

        return target;
    }

    //你的回合
    public void YourRound()
    {
        //我的回合!
        RoundManager.SetOrigin(entity);

        //意图技能
        RoundManager.SetSkill(IntentionList[0].skill);
        if (IntentionList[0].skill.targetType != Skill.TargetType.self)
        {

            //选择目标
            Entity target = FindTarget();
            RoundManager.SetTarget(target);
        }

        //移除生效意图，并刷新
        IntentionList.RemoveAt(0);
        UpToNumber();
        Refresh();
    }
}
