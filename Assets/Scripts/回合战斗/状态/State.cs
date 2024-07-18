using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public StateChecker stateChecker;

    public StateManager stateManager;

    public int stackCount;

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

    private void OnValidate()
    {
        stateChecker = GetComponent<StateChecker>();
    }

    public virtual void OnSideOur(Entity entity)
    {

    }

    //减少状态层数
    public virtual void DetectStackCount()
    {
        DetectStackCount(1);
    }

    //减少状态层数
    public virtual void DetectStackCount(int value)
    {
        StackCount -= value;
        if (StackCount <= 0)
        {
            stateManager.stateList.Remove(this);
            Destroy(this.gameObject);
        }
    }
}
