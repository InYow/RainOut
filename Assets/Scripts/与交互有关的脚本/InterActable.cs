using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InterActable : MonoBehaviour
{
    public bool EnterBound;
    public bool ExitBound;
    private void Awake()
    {
        EnterBound = false;
        ExitBound = true;
    }
    private void LateUpdate()
    {
        if (EnterBound && ExitBound)
        {
            CheckExit();
            EnterBound = false;
        }
        ExitBound = true;
    }
    //交互的类型
    public enum Intertype
    {
        Nullable,
        Pickable_Weapon,
        Pickable_Item,
        Contact_Talk
    }
    //执行交互
    public virtual Intertype Active()
    {
        Debug.Log($"{gameObject}发生交互了");
        return Intertype.Nullable;
    }
    public virtual void Check()
    {
        if (!EnterBound)
        {
            CheckEnter();
        }
        else
        {
            CheckStay();
        }
        EnterBound = true;
        ExitBound = false;
    }
    public virtual void CheckStay()
    {
        //        Debug.Log($"{gameObject} CheckStay");
        return;
    }
    public virtual void CheckEnter()
    {

    }
    public virtual void CheckExit()
    {

    }
}
