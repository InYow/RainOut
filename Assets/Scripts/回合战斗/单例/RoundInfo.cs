using System;
using UnityEngine;

[Serializable]
public class RoundInfo
{
    public Entity origin;

    public Entity target;

    public Skill.TargetType skill_targetType;

    public string skillname;

    public RoundType roundType;

    public RoundInfo(Entity o, Entity t, Skill.TargetType type, string skillname, RoundType roundType)
    {
        origin = o;
        target = t;
        skill_targetType = type;
        this.skillname = skillname;
        this.roundType = roundType;
    }
}