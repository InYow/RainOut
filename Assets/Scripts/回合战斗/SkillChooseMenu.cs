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
    public List<SkillChooseBtn> BtnList;

    private void Start()
    {
        PrepareSkillBtn();
    }

    private void OnEnable()
    {
        var roundtype = RoundManager.roundManager.roundType;//回合类型

        //自然回合
        if (roundtype == RoundType.normal)
        {
            //全部打开
            foreach (var btn in BtnList)
            {
                btn.button.interactable = true;
            }
        }

        //额外回合
        else if (roundtype == RoundType.more)
        {
            var history = RoundManager.HisToryGet();//历史列表
            var origin = RoundManager.roundManager.originEntity;//行动者

            //对于每一个有技能的技能按钮
            foreach (var btn in BtnList)
            {
                if (btn.Skill != null)
                {
                    string skillname_btn = btn.Skill.skillName;//技能名-按钮

                    //对于每一个按钮都要在history里判断一边
                    foreach (var roundinfo in history)
                    {
                        if (roundinfo.origin == origin /*同一个人*/ && roundinfo.skillname == skillname_btn /*同一个技能*/ )
                        {
                            //禁用
                            btn.button.interactable = false;
                        }
                        if (roundinfo.roundType == RoundType.normal)
                        {
                            //遍历停止
                            break;
                        }
                    }
                }
            }
        }
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

    //技能冷却完成
    public void CoolDownFinish()
    {
    }

    //
}
