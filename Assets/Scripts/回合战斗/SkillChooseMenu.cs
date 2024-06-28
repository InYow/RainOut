using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillChooseMenu : MonoBehaviour
{
    //输入的技能列表
    public List<Skill> SkillList;

    //管辖的按钮列表
    public List<ChooseBtn> BtnList;

    private void Start()
    {
        PrepareSkillBtn();
    }

    //为技能选择按钮赋值，做好准备
    public void PrepareSkillBtn()
    {
        int i = 0;
        foreach (var skill in SkillList)
        {
            BtnList[i].Skill = skill;
            i++;
        }
        for (; i < BtnList.Count;)
        {
            BtnList[i].Skill = null;
            i++;
        }
    }
}
