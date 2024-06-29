using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDetailChecker : DetailChecker
{
    public void SetSkill(Skill skill)
    {
        SetTextDes(skill.skillDes);
    }
}
