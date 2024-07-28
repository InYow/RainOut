using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [Header("必备变量")]

    public StateChecker stateChecker;

    public StateManager stateManager;

    [Header("自己信息")]

    public int stackCount;

    //状态计数
    public int StackCount
    {
        get
        {
            return stackCount;
        }
        set
        {
            stackCount = value;
            stateChecker.stackCountGUI.text = $"{value}";
        }
    }

    private void Start()
    {
        StackCount = StackCount;
    }

    private void OnValidate()
    {
        stateChecker = GetComponent<StateChecker>();
    }

    //计数减一
    public void DetectStackCount()
    {
        DetectStackCount(1);
    }

    //计数减少
    public void DetectStackCount(int value)
    {
        StackCount -= value;
        if (StackCount <= 0)
        {
            stateManager.stateList.Remove(this);
            Destroy(this.gameObject);
        }
    }

    //-----------------------------------------------------------------------------------
    //重写区
    //------------------------------------------------------------------------------------

    //行动到边（在自动生成回合时，紧随生成回合后调用）
    public virtual void OnSideOur(Entity entity)
    {

    }

    //受到攻击伤害时调用
    public virtual void OnAttackGet(Entity entity, ref float minuend, Skill skill)
    {

    }
}
