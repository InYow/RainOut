using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEventForEntity : MonoBehaviour
{
    public Entity entity;
    private void OnValidate()
    {
        entity = GetComponentInChildren<Entity>();
    }
    public void AnimaSkill()
    {
        entity.AnimaSkill();
    }
}
