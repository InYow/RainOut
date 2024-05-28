using System;
public class DialogChoiceActionInfo
{
    public string text;
    public Action action;
    public DialogChoiceActionInfo(string t, Action a)
    {
        text = t;
        action = a;
    }
}