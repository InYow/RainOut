using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectEntity : MonoBehaviour, IPointClickInterface
{
    [Tooltip("绑定的entity")] public Entity entity;

    [Tooltip("是否可以使用")] public bool interactable = true;

    public enum Type
    {
        origin = 0,
        target
    }

    //出现的意义，行动模式
    public Type type;

    public bool Interactable
    {
        get
        {
            return interactable;
        }
        set
        {
            interactable = value;
            if (value == true)
            {
                spriteRenderer.color = NormalColor;
            }
            else
            {
                spriteRenderer.color = DisableColor;
            }
        }
    }

    [Tooltip("点击事件")] public UnityEvent OnClick;

    [Tooltip("精灵渲染器")] public SpriteRenderer spriteRenderer;

    [Header("视觉表现相关")]

    [Tooltip("偏移量")] public Vector2 offset;

    [Tooltip("旋转速度")] public float rotateSpeed;

    [Tooltip("平常颜色")] public Color NormalColor;

    [Tooltip("悬浮颜色")] public Color HoverColor;

    [Tooltip("禁用颜色")] public Color DisableColor;


    public void PointClick()
    {
    }

    public void PointClickDown()
    {
        //Debug.Log("选择了" + entity);
        OnClick.Invoke();
        RoundManager.SelectEntity(entity);
    }

    public void PointClickEnter()
    {
        spriteRenderer.color = HoverColor;
    }

    public void PointClickExit()
    {
        spriteRenderer.color = NormalColor;
    }

    public void PointClickHover()
    {
    }

    public void PointClickUp()
    {
    }

    public void OnManagerSelect()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    private void OnValidate()
    {
        //位置
        transform.position = entity.gameObject.transform.position + (Vector3)offset;

        //名称
        gameObject.name = entity.entityName;

        //赋值entity
        entity.selectEntity = this;
    }
}
