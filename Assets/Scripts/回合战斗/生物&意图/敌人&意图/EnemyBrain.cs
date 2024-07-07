using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class EnemyBrain : MonoBehaviour
{
    [Tooltip("生物体")] public Entity enemy;

    [Tooltip("脑子模板")] public BrainTable brainTable;

    public IntentionChecker intentionCheckerPrb;

    [Tooltip("检视器数量")] public int number;

    [Tooltip("检视器存放位置")] public Transform CheckerListTrans;

    [Tooltip("检视器列表")] public List<IntentionChecker> intentionCheckerList;

    [Tooltip("行动意图列表")] public List<Intention> intentionList;


    private void OnValidate()
    {
        if (enemy == null)
        {
            enemy = GetComponent<Entity>();
        }
    }

    private void Start()
    {
        UpToNumber();
        Refresh();
    }

    //意图种类 权重, 生成模式, 索敌规则, 被打断的行动调整等数据, 放在BrainTable中
    //生成意图时, 传递场上信息参数, 由BrainTable执行内部方法, 并返回意图
    //this类是BrainTable的解释器
    [ContextMenu("动态生成意图")]
    public void UpToNumber()
    {
        //动态生成intentionChecker
        int i = intentionCheckerList.Count;
        for (; i < number; i++)
        {
            IntentionChecker intentionChecker = Instantiate(intentionCheckerPrb, CheckerListTrans);
            intentionCheckerList.Add(intentionChecker);
        }

        //观察到的意图数量
        int n = intentionCheckerList.Count;

        //动态生成intention
        for (; intentionList.Count < n;)
        {
            Intention intention = brainTable.RandomIntention();
            intentionList.Add(intention);
        }

    }

    [ContextMenu("刷新显示")]
    public void Refresh()
    {
        List<IntentionChecker> checkerList = intentionCheckerList;
        int i = 0;
        foreach (var checker in checkerList)
        {
            checker.SetIntention(intentionList[i]);
            i++;
        }
    }

    //你的回合
    public void YourRound()
    {
        //我的回合!
        RoundManager.SetOrigin(enemy);

        //意图技能
        RoundManager.SetSkill(intentionList[0].skill);
        if (intentionList[0].skill.targetType != Skill.TargetType.self)
        {
            //选择目标
            Entity target = FindTarget();
            RoundManager.SetTarget(target);
        }

        //移除生效意图，并刷新
        IntentionRemoveAt(0);
        UpToNumber();
        Refresh();
    }

    /// <summary>
    /// 删除意图
    /// </summary>
    /// <param name="index"></param>
    public void IntentionRemoveAt(int index)
    {
        intentionList.RemoveAt(index);
    }

    /// <summary>
    /// 删除到指定意图，包含
    /// </summary>
    public void IntentionRemoveTo(int index)
    {
        if (intentionList.Count > index)
        {
            for (; index >= 0; index--)
            {
                IntentionRemoveAt(0);
            }
        }
        else
        {
            throw new NotImplementedException("要删除的意图索引超出范围");
        }
    }

    //重写-----------------------------------------------------------------------------------------------

    /// <summary>
    /// 在这里调用索敌行为规则
    /// </summary>
    public virtual Entity FindTarget()
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

    //意图被打断
    public virtual void Break(int index)
    {
        //删除到被击破意图, 后续意图前移
        IntentionRemoveTo(index);

        //补全意图数量
        UpToNumber();
        //刷新显示
        Refresh();
    }

    [ContextMenu("打断第一")]
    public void Break01()
    {
        Break(0);
    }

    [ContextMenu("打断第二")]
    public void Break02()
    {
        Break(1);
    }

    [ContextMenu("打断第三")]
    public void Break03()
    {
        Break(2);
    }
}
