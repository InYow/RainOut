using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DialogMethod
{
    public static void LockChoice(Choice choice)
    {
        choice.Lock();
    }
    public static void UnLockChoice(Choice choice)
    {
        choice.UnLock();
    }
    public static void SetLockChoice(Choice choice, bool b)
    {
        choice.SetLock(b);
    }
}
