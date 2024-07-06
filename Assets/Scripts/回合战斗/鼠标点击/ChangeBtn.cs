using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBtn : PointClickButton
{
    [Header("对应的生物")]
    public Entity entity;

    private void Start()
    {
        if (entity == null)
        {
            Debug.Log("没有绑定entity" + gameObject.name);
        }
    }
}
