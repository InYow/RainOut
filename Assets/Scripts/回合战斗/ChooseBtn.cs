using System;
using System.Collections;
using System.Collections.Generic;
using SuperTiled2Unity.Editor.LibTessDotNet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseBtn : MonoBehaviour, IPointClickInterface
{
    public bool interactable;
    [Header("技能详情")]
    public SkillDetailChecker skillDetailCheckerPrb;

    public SkillDetailChecker skillDetailChecker;

    public Vector2 offset;

    [Header("组件")]
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

    public bool Interactable { get => interactable; set => interactable = value; }

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

    public void PointClickEnter()
    {
        if (skill != null)
        {
            //实例化
            skillDetailChecker = Instantiate(skillDetailCheckerPrb, transform);

            //位置
            skillDetailChecker.transform.localPosition += (Vector3)offset;
            //内容
            skillDetailChecker.SetSkill(skill);
        }
    }

    public void PointClickHover()
    {
    }

    public void PointClickExit()
    {
        if (skillDetailChecker != null)
        {
            Destroy(skillDetailChecker.gameObject);
            skillDetailChecker = null;
        }
    }

    public void PointClickDown()
    {
    }

    public void PointClick()
    {
    }

    public void PointClickUp()
    {
    }
}
