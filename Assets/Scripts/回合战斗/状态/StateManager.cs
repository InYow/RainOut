using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public List<State> stateList;

    [Header("状态栏")]
    public Transform stateTrs;

    //-----------------------------------------------
    //工具类方法
    //----------------------------------------------

    private void Start()
    {
        if (stateTrs == null)
        {
            throw new NotImplementedException("没有指定statesTrs");
        }
    }

    public State FindWithClassName(string class_name)
    {
        foreach (var state in stateList)
        {
            //比较字符串,不区分大小写
            if (string.Equals(state.GetType().Name, class_name, StringComparison.OrdinalIgnoreCase))
            {
                return state;
            }
        }
        return null;
    }

    //-------------------------------------------------------------
    //公开的方法
    //-------------------------------------------------------------

    //生成该生成的状态
    public void StateAdd(State s, int stack)
    {
        State state_has = FindWithClassName(s.GetType().Name);
        if (state_has == null)
        {
            //没有同名状态,生成一个

            State state = Instantiate(s, stateTrs);
            stateList.Add(state);

            state.stateManager = this;
            state.StackCount = stack;
        }
        else
        {
            //有同名状态,计数叠加,但不超过99
            state_has.StackCount += stack;
            if (state_has.StackCount > 99)
                state_has.StackCount = 99;
        }
    }

    //见State.OnSideOur
    public void SideOur(Entity entity)
    {
        for (int i = stateList.Count - 1; i >= 0; i--)
        {
            stateList[i].OnSideOur(entity);
        }

        #region 创建一个新的列表存储需要删除的元素
        // List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        // List<int> toRemove = new List<int>();

        // foreach (var number in numbers)
        // {
        //     if (number % 2 == 0)
        //     {
        //         toRemove.Add(number);
        //     }
        // }

        // foreach (var number in toRemove)
        // {
        //     numbers.Remove(number);
        // }

        // foreach (var number in numbers)
        // {
        //     Console.WriteLine(number);
        // }
        #endregion
    }

    //受到攻击时调用
    public void AttackGet(Entity entity, ref float minuend/*被减数*/, Skill skill)
    {
        for (int i = stateList.Count - 1; i >= 0; i--)
        {
            stateList[i].OnAttackGet(entity, ref minuend, skill);
        }
    }
}
