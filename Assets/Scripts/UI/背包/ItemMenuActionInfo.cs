using System;
public class ItemMenuActionInfo
{
    public string text;
    public Action action;
    public ItemMenuActionInfo(string t, Action a)
    {
        text = t;
        action = a;
    }
}