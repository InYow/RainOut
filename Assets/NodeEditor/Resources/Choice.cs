using System;
using UnityEngine;
[Serializable]
public class Choice : ScriptableObject
{
    public Paragraph prg;
    public string Text;
    public bool locked;
    public string LockText;
    public void UnLock()
    {
        locked = false;
    }
    public void Lock()
    {
        locked = true;
    }
    /// <summary>
    /// true == 上锁的, false == 解锁的
    /// </summary>
    /// <param name="b"></param>
    public void SetLock(bool b)
    {
        if (b)
        {
            Lock();
        }
        else
        {
            UnLock();
        }
    }
}