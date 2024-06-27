using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectEntity : MonoBehaviour, IPointClickInterface
{
    [Tooltip("绑定的entity")] public Entity entity;

    [Tooltip("精灵渲染器")] public SpriteRenderer spriteRenderer;

    [Header("视觉表现相关")]
    [Tooltip("旋转速度")] public float rotateSpeed;

    [Tooltip("平常颜色")] public Color unHoverColor;

    [Tooltip("悬浮颜色")] public Color HoverColor;

    public void PointClick()
    {
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
        spriteRenderer.color = unHoverColor;
    }

    public void PointClickHover()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void PointClickUp()
    {
    }
}
