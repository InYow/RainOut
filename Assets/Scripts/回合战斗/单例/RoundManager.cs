using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.UIElements;

//还承担了控制selectentity圆圈的任务
public class RoundManager : MonoBehaviour
{
    public static RoundManager roundManager;

    //回合预制体
    public Round roundPrb;

    public MoreRound moreRoundPrb;

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

    //行动方
    public Side originSide;

    //行动方
    public Side OriginSide { get { return originSide; } set { originSide = value; } }

    //未行动的自然回合队列
    public List<Round> RoundList;

    //额外回合队列
    public List<MoreRound> MoreList;

    //换手按钮队列
    public List<ChangeBtn> ChangeBtnList;

    //当前回合主人身上的圆圈
    public SelectEntity originEntitySelect;

    [Header("三方信息")]

    public Round currentRound;

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

    //获取AList
    public static List<Entity> GetAList()
    {
        return roundManager.AList;
    }

    //所有圆圈可见/不可见
    public static void SelectAllSet(bool b)
    {
        foreach (var e in roundManager.AList)
        {
            e.selectEntity.gameObject.SetActive(b);
        }
        foreach (var e in roundManager.BList)
        {
            e.selectEntity.gameObject.SetActive(b);
        }
    }

    //圆圈可见/不可见
    public static void SelectSet(Entity e, bool b)
    {
        e.selectEntity.gameObject.SetActive(b);
    }

    //拥有自然回合的可见/不可见
    public static void SelectHasRoundSet(bool b)
    {
        foreach (var r in roundManager.RoundList)
        {
            r.master.selectEntity.gameObject.SetActive(b);
        }
    }

    //换手按钮全部打开/关闭
    public static void ChangeBtnAllSet(bool b)
    {
        foreach (var btn in roundManager.ChangeBtnList)
        {
            btn.gameObject.SetActive(b);
        }
    }

    public static void ChangeBtnSet(Entity e, bool b)
    {
        bool has = false;

        foreach (var btn in roundManager.ChangeBtnList)
        {
            if (btn.entity == e)
            {
                btn.gameObject.SetActive(b);
                has = true;
                break;
            }
        }

        if (!has)
        {
            throw new NotImplementedException($"目标{e.gameObject.name}不存在");
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

    //添加自然可行回合
    public static void RoundAddList(Round round)
    {
        //添加
        roundManager.RoundList.Add(round);
    }

    //移除自然可行回合
    public static void RoundRemoveList(Entity entity)
    {
        bool b = true;

        for (int i = 0; i < roundManager.RoundList.Count; i++)
        {
            if (roundManager.OriginEntity == roundManager.RoundList[i].master)
            {
                b = false;
                roundManager.RoundList.RemoveAt(i);
                break;
            }
        }

        if (b)
        {
            throw new NotImplementedException("移除回合不存在");
        }
    }

    public static void RoundRemoveList(Round round)
    {
        MoreRound moreRound = round as MoreRound;
        if (moreRound != null)
        {
            roundManager.MoreList.Remove(moreRound);
        }
        else
        {
            roundManager.RoundList.Remove(round);
        }
    }

    //增加额外回合
    public static void MoreAdd(Entity e)
    {
        MoreRound moreRound = MoreRound.Create(roundManager.moreRoundPrb, roundManager.transform, e, 0);
        roundManager.MoreList.Add(moreRound);
    }

    public static void MoreChange(Entity e)
    {
        MoreRound moreRound = roundManager.currentRound as MoreRound;
        if (moreRound != null)
        {
            //回合之前的主人
            Entity e_before = moreRound.master;
            //改变额外回合的主人
            moreRound.gameObject.name = e.entityName;
            moreRound.master = e;

            //行动者和圆圈
            roundManager.OriginEntity = e;
            roundManager.originEntitySelect = e.selectEntity;

            //圆圈只打开
            SelectAllSet(false);
            SelectSet(roundManager.OriginEntity, true);

            //新主人的四个块
            roundManager.originEntity.selectEntity.OnClick.Invoke();

            //换手按钮全部关闭
            ChangeBtnAllSet(false);
        }
        else
        {
            throw new NotImplementedException("没有额外回合可用");
        }
    }

    [ContextMenu("先手开始战斗")]
    public void RS1()
    {
        BattleStart(1);
    }
    [ContextMenu("后手开始战斗")]
    public void RS2()
    {
        BattleStart(2);
    }

    /// <summary>
    /// 回合战斗开始
    /// </summary>
    /// <param name="Type">1 先手, 2 后手</param>
    public static void BattleStart(int Type)
    {
        switch (Type)
        {
            //先手
            case 1:
                {
                    //生成回合并Add至列表
                    int i = 1;
                    foreach (var entity in roundManager.AList)
                    {
                        //回合
                        if (!entity.dead)
                        {
                            Round round = Round.Create(roundManager.roundPrb, roundManager.transform, entity, i);
                            RoundAddList(round);
                            i++;
                        }
                    }
                    break;
                }

            //后手
            case 2:
                {
                    //生成回合并Add至列表
                    int i = 1;
                    foreach (var entity in roundManager.BList)
                    {
                        //回合
                        if (!entity.dead)
                        {
                            Round round = Instantiate(roundManager.roundPrb, roundManager.transform);
                            round.gameObject.name = entity.entityName;
                            round.master = entity;
                            round.id = i;
                            RoundAddList(round);
                            i++;
                        }
                    }
                    break;
                }

            default:
                {
                    break;
                }
        }
        InitRound();
    }

    //换边
    public static void SideChange(Side side)
    {
        roundManager.OriginSide = side;
        switch (side)
        {
            case Side.A:
                {
                    foreach (var e in roundManager.AList)
                    {
                        //回合
                        if (!e.dead)
                        {
                            Round round = Instantiate(roundManager.roundPrb, roundManager.transform);
                            round.gameObject.name = e.entityName;
                            round.master = e;
                            RoundAddList(round);
                        }
                    }
                    break;
                }
            case Side.B:
                {
                    foreach (var e in roundManager.BList)
                    {
                        //回合
                        if (!e.dead)
                        {
                            Round round = Instantiate(roundManager.roundPrb, roundManager.transform);
                            round.gameObject.name = e.entityName;
                            round.master = e;
                            RoundAddList(round);
                        }
                    }
                    break;
                }
            default:
                break;
        }
    }

    //上个回合结束后，下个回合开始前的初始化
    public static void InitRound()
    {
        roundManager.currentRound = null;
        roundManager.OriginEntity = null;
        roundManager.Skill = null;
        roundManager.targetEntity = null;

        //优先使用额外回合
        if (roundManager.MoreList.Count != 0)
        {
            roundManager.currentRound = roundManager.MoreList[0];
            roundManager.OriginEntity = roundManager.MoreList[0].master;

            //打开四个块
            roundManager.OriginEntity.selectEntity.OnClick.Invoke();

            //圆圈只打开
            SelectAllSet(false);
            SelectSet(roundManager.OriginEntity, true);

            //换手所有打开，除了自己
            ChangeBtnAllSet(true);
            ChangeBtnSet(roundManager.OriginEntity, false);
        }
        else
        {
            //无额外回合

            //圆圈正常打开
            SelectHasRoundSet(true);
        }

        if (roundManager.OriginSide == RoundManager.Side.B)
        {
            roundManager.RoundList[0].master.gameObject.GetComponent<EnemyBrain>().YourRound();
        }
    }

    //技能的释放目标
    public static void SelectEntity(Entity entity)
    {
        //改变战斗者
        if (roundManager.AList.Contains(entity))
        {
            SetOrigin(entity);
        }
        //选定攻击者，并施法
        else
        {
            SetTarget(entity);
        }
    }

    //一
    /// <summary>
    /// 在自然回合队列中选择回合的拥有者
    /// </summary>
    /// <param name="entity"></param>
    public static void SetOrigin(Entity entity)
    {
        roundManager.OriginEntity = entity;
        roundManager.originEntitySelect = entity.selectEntity;

        Round round = null;
        foreach (var r in roundManager.MoreList)
        {
            if (r.master == entity)
            {
                round = r;
                break;
            }
        }
        if (round == null)
        {
            foreach (var r in roundManager.RoundList)
            {
                if (r.master == entity)
                {
                    round = r;
                    break;
                }
            }
        }

        roundManager.currentRound = round;
    }

    //二
    //释放技能
    public static void SetSkill(Skill skill)
    {
        roundManager.Skill = skill;

        //仅需要施法者的技能
        roundManager.Skill.SetOrigin(roundManager.OriginEntity);
    }

    //三
    //释放目标
    public static void SetTarget(Entity target)
    {
        //设置目标
        roundManager.targetEntity = target;
        //使用技能
        roundManager.Skill.SetOriginAndTarget(roundManager.OriginEntity, roundManager.targetEntity);
    }

    //四
    //动画触发技能效果
    public static void AnimaSkillEffect()
    {
        roundManager.Skill.AnimaSkillEffect();
    }

    //五
    /// <summary>
    /// 回合结束
    /// </summary>
    public static void RoundFinish()
    {
        //移除当前回合
        RoundRemoveList(roundManager.currentRound);

        //一边是否结束
        if (roundManager.RoundList.Count == 0 && roundManager.MoreList.Count == 0)
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

        //执行回合结束后的流程
        InitRound();
    }

    //尝试结束战斗
    public static void BattleEnd()
    {
        bool B_Out = true;

        foreach (var entity in roundManager.BList)
        {
            if (!entity.dead)
            {
                B_Out = false;
                break;
            }
        }

        if (B_Out)
        {
            //B队活光光
            Debug.Log("你赢了");
        }
        else
        {
            //无事发生
        }
    }

    //失败了
    public static void BattleFailure()
    {
        Debug.Log("你败了");
    }
}
