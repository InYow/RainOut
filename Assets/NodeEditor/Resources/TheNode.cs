using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TheNode : ScriptableObject
{
    public string Speaker;//说话的人
    public Sprite Emotion;//立绘表情 
    [TextArea(3, 10)]
    public string Text;//文本内容
    public bool Readed = false;
}
