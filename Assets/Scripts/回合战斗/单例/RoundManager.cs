using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

//还承担了控制selectentity圆圈的任务
public class RoundManager : MonoBehaviour
{
    public static RoundManager roundManager;

    //回合预制体
    public Round roundPrb;

    //玩家控制的角色们
    public List<Entity> AList;

    //玩家对阵的敌人们
    public List<Entity> BList;

    //行动方枚举定义
    public enum Side
    {
        A = 0,
        B
    }

    public Side originSide;

    //行动方
    public Side OriginSide { get { return originSide; } set { originSide = value; } }

    //未行动的回合队列
    public List<Round> RoundList;

    //当前回合主人身上的圆圈
    public SelectEntity originEntitySelect;

    [Header("三方信息")]

    //当前回合的主人,技能的释放者
    public Entity originEntity;
    public Entity OriginEntity
    {
        get
        {
            return originEntity;
        }
        set
        {
            //判断值是否有变化
            if (originEntity == value)
            {

            }
            else
            {
                originEntity = value;
                if (value == null)
                {
                    originEntitySelect = null;

                }
                else
                {
                    originEntitySelect = value.selectEntity;
                }
                //更深层级的值
                //技能, 目标 清空
                Skill = null;
                targetEntity = null;
            }
        }
    }

    //释放的技能
    public Skill skill;

    //释放的技能 
    public Skill Skill
    {
        get
        {
            return skill;
        }
        set
        {
            skill = value;

            //设置的skill是否为空
            if (value != null)
            {
                //能成为skill目标的圆圈可见
                switch (OriginSide)
                {
                    case Side.A:
                        {
                            foreach (var entity in BList)
                            {
                                if (value.IfCanTarget(entity))
                                {
                                    entity.selectEntity.gameObject.SetActive(true);
                                }
                            }
                            break;
                        }

                    case Side.B:
                        {
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                //关闭该关闭的圆圈
                switch (OriginSide)
                {
                    case Side.A:
                        {
                            foreach (var entity in BList)
                            {
                                entity.selectEntity.gameObject.SetActive(false);
                            }
                            break;
                        }

                    case Side.B:
                        {
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
        }
    }

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
    private void Update()
    {
        originEntitySelect?.OnManagerSelect();
    }

    //初始化,清空上次对局的信息
    public static void Init()
    {
        roundManager.AList.Clear();
        roundManager.BList.Clear();
        roundManager.OriginEntity = null;
        roundManager.Skill = null;
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

    //entity还有回合
    public static bool RoundContains(Entity entity)
    {
        foreach (var round in roundManager.RoundList)
        {
            if (round.master == entity)
                return true;
        }
        return false;
    }

    //添加可行回合
    public static void RoundAddList(Round round)
    {
        //添加
        roundManager.RoundList.Add(round);

        //可行回合主人圆圈可见
        round.master.selectEntity.gameObject.SetActive(true);
    }

    //移除可行回合
    public static void RoundRemoveList(int index)
    {
        //可行回合主人圆圈不可见
        roundManager.RoundList[index].master.selectEntity.gameObject.SetActive(false);

        //移除
        roundManager.RoundList.RemoveAt(index);

        //一边是否结束
        if (roundManager.RoundList.Count == 0)
        {
            //换边
            switch (roundManager.originSide)
            {
                case Side.A:
                    {
                        SideChange(Side.B);
                        break;
                    }
                case Side.B:
                    {
                        SideChange(Side.A);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

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
                        RoundAddList(round);
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
                        RoundAddList(round);
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

    //换边
    public static void SideChange(Side side)
    {

    }

    //上个回合结束后，下个回合开始前的初始化
    public static void InitRound()
    {
        roundManager.OriginEntity = null;
        roundManager.Skill = null;
        roundManager.targetEntity = null;
    }

    //释放的技能
    public static void SetSkill(Skill skill)
    {
        roundManager.Skill = skill;
    }

    //技能的释放目标
    public static void SelectEntity(Entity entity)
    {
        //改变战斗者
        if (roundManager.AList.Contains(entity))
        {
            roundManager.OriginEntity = entity;
            roundManager.originEntitySelect = entity.selectEntity;
        }
        //选定攻击者，并施法
        else
        {
            //设置目标
            roundManager.targetEntity = entity;
            //使用技能
            roundManager.Skill.SetOriginAndTarget(roundManager.OriginEntity, roundManager.targetEntity);
            //回合已经用掉了
            for (int i = 0; i < roundManager.RoundList.Count; i++)
            {
                if (roundManager.OriginEntity == roundManager.RoundList[i].master)
                {
                    RoundRemoveList(i);
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
