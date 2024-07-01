using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DetailChecker : MonoBehaviour
{
    public RectTransform rectTransform;

    public TextMeshProUGUI DesTextGUI;

    [TextArea(3, 10)]
    public string text;

    [Header("文本框自动长度")]
    [Tooltip("当前文字数量")] public float count;

    [Tooltip("文本框长度")] public float length;

    [Tooltip("系数偏移量")] public float offset_k = 0f;

    [Tooltip("b值偏移量")] public float offset_b = 0f;

    [Tooltip("汉字计值")] public float full_Value = 1f;

    [Tooltip("字母计值")] public float half_Value = 0.5f;

    public void OnValidate()
    {
        if (rectTransform != null)
        {
            EditorApplication.delayCall += () =>
             {
                 SetTextDes(text);
             };
        }
    }

    public void SetTextDes(string str)
    {
        // 更新文字
        if (DesTextGUI != null)
        {
            DesTextGUI.text = str;
            Canvas.ForceUpdateCanvases();
        }

        // 更新长度
        if (rectTransform != null)
        {
            // 计算长度
            float x = CountCharacters(str);
            float y = (1 / 3f + offset_k) * x + 2f / 3f + offset_b;
            if (y < 1f)
            {
                y = 1f;
            }
            count = x;
            length = y;

            Vector2 vector2 = rectTransform.sizeDelta;
            vector2.x = y;
            rectTransform.sizeDelta = vector2;

            // 强制更新布局
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }

    //文字数-x, 长度-y
    //1 1
    //4 2
    //7 3
    //  8
    //y=1/3x+2/3);

    /// <summary>
    /// 计算字符串有多少个字符，全角计1，半角计0.5
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    protected float CountCharacters(string str)
    {
        float count = 0;

        foreach (char c in str)
        {
            if (IsFullWidth(c))
            {
                count += full_Value;
            }
            else
            {
                count += half_Value;
            }
        }

        return count;

        //规则
        static bool IsFullWidth(char c)
        {
            //return c >= '\uFF01' && c <= '\uFF5E' || c >= '\u3000' && c <= '\u303F';
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
            {
                return false;
            }
            else if (c >= '\u0020' && c <= '\u007E')
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}