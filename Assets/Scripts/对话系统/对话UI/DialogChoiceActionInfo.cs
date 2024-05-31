using System;
public class DialogChoiceActionInfo
{
    public string text;
    public Choice choice;
    /// <summary>
    /// 效果为进入对应段落Paragraph
    /// </summary>
    public Action action;

    public DialogChoiceActionInfo(string t, Choice c, Action a)
    {
        text = t;
        this.choice = c;
        action = a;
    }
}