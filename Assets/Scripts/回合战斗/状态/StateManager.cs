using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public List<State> stateList;

    public void SideOur(Entity entity)
    {
        foreach (var state in stateList)
        {
            state.OnSideOur(entity);
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
}
