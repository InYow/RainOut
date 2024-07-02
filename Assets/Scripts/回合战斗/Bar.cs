using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image image;

    public TextMeshProUGUI textGUI;


    //执行Bar的任务
    public void AsBar(string str, float percent)
    {
        //文字更新
        textGUI.text = str;

        //图片填充比例更新
        image.fillAmount = percent;
    }
}
