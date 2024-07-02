using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityInfoManager : MonoBehaviour
{
    //生物
    public Entity entity;

    //生命条
    public HealthBar healthBar;

    //护甲块
    public EntityArmor entityArmor;

    //状态栏

    //详细信息显示

    private void OnValidate()
    {
        entity = GetComponentInParent<Entity>();
    }
}
