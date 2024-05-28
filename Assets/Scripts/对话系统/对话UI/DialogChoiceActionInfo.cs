using System;
public class DialogChoiceActionInfo
{
    public string text;
    /// <summary>
    /// 效果为进入对应段落Paragraph
    /// </summary>
    public Action action;

    public DialogChoiceActionInfo(string t, Action a)
    {
        text = t;
        action = a;
    }
}