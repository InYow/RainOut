using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public static RoundManager roundManager;

    // 当前回合的主人,技能的释放者
    public Entity originEntity;

    //技能的释放目标
    public Entity selectEntity;

    //释放的技能
    public Skill skill;

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
        roundManager.selectEntity = null;
    }

    //设置释放的技能
    public static void Skill(Skill skill)
    {
        roundManager.skill = skill;
    }

    //设置技能的释放对象
    public static void SelectEntity(Entity entity)
    {
        roundManager.selectEntity = entity;

        //使用技能
        roundManager.skill.SetOriginAndTarget(roundManager.originEntity, roundManager.selectEntity);
    }
}
