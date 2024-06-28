using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectEntity : MonoBehaviour, IPointClickInterface
{
    [Tooltip("是否可以使用")] public bool interactable = true;

    public bool Interactable
    {
        get
        {
            return interactable;
        }
        set
        {
            interactable = value;
        }
    }

    [Tooltip("点击事件")] public UnityEvent OnClick;

    [Tooltip("绑定的entity")] public Entity entity;

    [Tooltip("精灵渲染器")] public SpriteRenderer spriteRenderer;

    [Header("视觉表现相关")]

    [Tooltip("旋转速度")] public float rotateSpeed;

    [Tooltip("平常颜色")] public Color NormalColor;

    [Tooltip("悬浮颜色")] public Color HoverColor;

    [Tooltip("禁用颜色")] public Color DisableColor;


    public void PointClick()
    {
        OnClick.Invoke();
    }

    public void PointClickDown()
    {
        Debug.Log("选择了" + entity);
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
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void PointClickUp()
    {
    }
}
