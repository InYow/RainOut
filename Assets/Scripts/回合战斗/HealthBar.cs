using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image image;

    public TextMeshProUGUI textGUI;

    public Entity entity;

    void Update()
    {
        //检查有无指定entity
        if (entity != null)
        {
            Bar();
        }
    }

    //执行Bar的任务
    public void Bar()
    {
        //文字更新
        string str = $"{entity.hp}/{entity.hpMax}";
        textGUI.text = str;

        //图片填充比例更新
        float percent = (float)entity.hp / (float)entity.hpMax;
        image.fillAmount = percent;
    }
}
