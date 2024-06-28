using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBtn : MonoBehaviour
{
    public Button button;

    public TextMeshProUGUI textGUI;

    [SerializeField]
    [Tooltip("对应的技能")] private Skill skill;

    public Skill Skill
    {
        get
        {
            return skill;
        }
        set
        {
            skill = value;

            //根据输入的值更新内容
            if (value != null)
            {
                textGUI.text = value.skillName;
            }
            else
            {
                button.interactable = false;
                textGUI.text = "";
            }
        }
    }

    //脚本编译或变量更改时调用
    private void OnValidate()
    {
        if (button == null)
            button = GetComponent<Button>();
        if (textGUI == null)
            textGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ChooseSkill()
    {
        RoundManager.SetSkill(skill);
    }
}
