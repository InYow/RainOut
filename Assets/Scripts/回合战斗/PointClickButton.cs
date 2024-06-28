using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PointClickButton : MonoBehaviour, IPointClickInterface
{
    public enum RenderType
    {
        color = 0,
    }

    public RenderType renderType = 0;

    public UnityEvent unityEvent;

    public SpriteRenderer spriteRenderer;

    public Color HoverColor;

    public Color PressColor;

    public Color NormalColor;

    [Header("开发调试")]
    public bool hover = false;

    public bool press = false;

    public void PointClick()
    {
        unityEvent.Invoke();
    }

    public void PointClickDown()
    {
        press = true;
        switch (renderType)
        {
            case 0:
                {
                    spriteRenderer.color = PressColor;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void PointClickEnter()
    {
        hover = true;

        switch (renderType)
        {
            case 0:
                {
                    spriteRenderer.color = HoverColor;
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void PointClickExit()
    {
        hover = false;
        switch (renderType)
        {
            case 0:
                {
                    if (press)
                    {
                        spriteRenderer.color = PressColor;
                    }
                    else
                    {
                        spriteRenderer.color = NormalColor;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    public void PointClickHover()
    {
    }

    public void PointClickUp()
    {
        press = false;
        switch (renderType)
        {
            case 0:
                {
                    if (hover)
                    {
                        spriteRenderer.color = HoverColor;
                    }
                    else
                    {
                        spriteRenderer.color = NormalColor;
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
