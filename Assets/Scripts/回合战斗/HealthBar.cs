using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : Bar
{
    [Header("管理器")]
    public EntityInfoManager entityInfoManager;

    [Header("生物")]
    public Entity entity;

    void Update()
    {
        //提取entity
        Entity e = null;
        if (entityInfoManager != null && entityInfoManager.entity != null)
        {
            e = entityInfoManager.entity;
        }
        else if (entity != null)
        {
            e = entity;
        }

        //操作
        if (e != null)
        {
            AsBar($"{e.hp}/{e.hpMax}", (float)e.hp / (float)e.hpMax);
        }
        else
        {
            throw new NotImplementedException("${gameObject.name} 的 entity||entityInfoManger 未赋值");
        }
    }

    private void OnValidate()
    {
        entityInfoManager = GetComponentInParent<EntityInfoManager>();
    }
}
