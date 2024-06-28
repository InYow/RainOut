using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager roundManager;

    //回合预制体
    public Round roundPrb;

    //玩家控制的角色们
    public List<Entity> AList;

    //玩家对阵的敌人们
    public List<Entity> BList;

    //未行动的回合队列
    public List<Round> RoundList;

    // 当前回合的主人,技能的释放者
    public Entity originEntity;

    //释放的技能
    public Skill skill;

    //技能的释放目标
    public Entity targetEntity;

    private void Awake()
    {
        //单例模式
        if (roundManager == null)
        {
            roundManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //
    }

    //初始化,清空上次对局的信息
    public static void Init()
    {
        roundManager.AList.Clear();
        roundManager.BList.Clear();
        roundManager.originEntity = null;
        roundManager.skill = null;
        roundManager.targetEntity = null;
    }

    //上个回合结束后，下个回合开始前的初始化
    public static void InitRound()
    {
        roundManager.originEntity = null;
        roundManager.skill = null;
        roundManager.targetEntity = null;
    }

    //向AList中添加Entity
    public static void AddToAList(Entity entity)
    {
        if (!roundManager.AList.Contains(entity))
        {
            roundManager.AList.Add(entity);
        }
    }

    //向BList中添加Entity
    public static void AddToBList(Entity entity)
    {
        if (!roundManager.BList.Contains(entity))
        {
            roundManager.BList.Add(entity);
        }
    }

    [ContextMenu("先手开始战斗")]
    public void RS1()
    {
        RoundStart(1);
    }
    [ContextMenu("后手开始战斗")]
    public void RS2()
    {
        RoundStart(2);
    }

    /// <summary>
    /// 回合战斗开始
    /// </summary>
    /// <param name="Type">1 先手, 2 后手</param>
    public static void RoundStart(int Type)
    {
        switch (Type)
        {
            //先手
            case 1:
                {
                    int i = 1;
                    foreach (var entity in roundManager.AList)
                    {
                        //回合
                        Round round = Instantiate(roundManager.roundPrb, roundManager.transform);
                        round.gameObject.name = entity.entityName;
                        round.master = entity;
                        round.id = i;
                        roundManager.RoundList.Add(round);
                        i++;
                    }
                    break;
                }

            //后手
            case 2:
                {
                    int i = 1;
                    foreach (var entity in roundManager.BList)
                    {
                        //回合
                        Round round = Instantiate(roundManager.roundPrb, roundManager.transform);
                        round.gameObject.name = entity.entityName;
                        round.master = entity;
                        round.id = i;
                        roundManager.RoundList.Add(round);
                        i++;
                    }
                    break;
                }

            default:
                {
                    break;
                }

        }

    }

    //设置释放的技能
    public static void SetSkill(Skill skill)
    {
        roundManager.skill = skill;
    }

    //设置技能的释放对象
    public static void SelectEntity(Entity entity)
    {
        //改变战斗者
        if (roundManager.AList.Contains(entity))
        {
            roundManager.originEntity = entity;
        }
        //选定攻击者，并施法
        else
        {
            //设置目标
            roundManager.targetEntity = entity;
            //使用技能
            roundManager.skill.SetOriginAndTarget(roundManager.originEntity, roundManager.targetEntity);
            //回合已经用掉了
            for (int i = 0; i < roundManager.RoundList.Count; i++)
            {
                if (roundManager.originEntity == roundManager.RoundList[i].master)
                {
                    roundManager.RoundList.RemoveAt(i);
                    break;
                }
            }
            //关闭展开的UI
            UIManager.ClearList();
            //执行回合结束后的流程
            InitRound();
        }
    }
}
