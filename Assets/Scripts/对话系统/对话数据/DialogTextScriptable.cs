using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New DialogDate", menuName = "ScriptableObjects/Dialog_TextData", order = 1)]
public class DialogTextScriptable : ScriptableObject
{
    public Sprite speaker_Image;
    public string speaker_Name;
    [TextArea(3, 7)]
    public string text;
}
