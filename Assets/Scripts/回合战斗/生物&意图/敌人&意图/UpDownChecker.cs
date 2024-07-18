using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UpDownChecker : Checker
{
    public TextMeshProUGUI textGUI;

    public Sprite up;

    public Color upColor;

    public string upStr;

    public Sprite down;

    public Color downColor;

    public string DownStr;

    public int correction;

    public void SetInfo(float info)
    {
        int value = (int)Mathf.Round((info - 1f) * 100f);
        correction = value;

        textGUI.text = $"{Mathf.Abs(value)}";

        if (value > 0)
        {
            gameObject.SetActive(true);
            sprite = up;
            textGUI.color = upColor;
        }
        else if (value < 0)
        {
            gameObject.SetActive(true);
            sprite = down;
            textGUI.color = downColor;
        }
        else if (value == 0)
        {
            gameObject.SetActive(false);
        }
        Sprite();
    }

    public override void Text()
    {
        if (correction > 0)
            detailChecker.SetTextDes(upStr);
        else if (correction < 0)
            detailChecker.SetTextDes(DownStr);
    }
}
