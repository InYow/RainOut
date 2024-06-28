using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectEntity_New : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [Tooltip("绑定的entity")] public Entity entity;

    [Tooltip("精灵渲染器")] public Image image;

    [Header("视觉表现相关")]
    [Tooltip("旋转速度")] public float rotateSpeed;

    [Tooltip("平常颜色")] public Color unHoverColor;

    [Tooltip("悬浮颜色")] public Color HoverColor;

    public bool hover = false;

    private void Update()
    {
        UpdateHover();
    }

    public void UpdateHover()
    {
        if (hover)
        {
            OnPointerHover();
        }
        else
        {
            OnNotPointerHover();
        }
    }

    private void OnNotPointerHover()
    {
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void OnPointerHover()
    {
        //transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("选择了" + entity);
        //RoundManager.SelectEntity(entity);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = HoverColor;
        hover = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = unHoverColor;
        hover = false;
    }
}
