using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentionChecker : MonoBehaviour, IPointClickInterface
{
    [Header("意图")]
    public Intention intention;

    [Header("显示意图相关")]
    public SpriteRenderer spriteRenderer;

    [Tooltip("文本框")] public DetailChecker detailCheckerPrb;

    [Header("PointClick")]
    public bool interactable;

    public DetailChecker detailChecker;

    public bool Interactable { get => interactable; set => interactable = value; }

    public Vector2 offset;

    public void SetIntention(Intention intention)
    {
        this.intention = intention;
        spriteRenderer.sprite = intention.sprite;
    }

    public void PointClick()
    {
    }

    public void PointClickDown()
    {
    }

    public void PointClickEnter()
    {
        //实例化
        detailChecker = Instantiate(detailCheckerPrb, transform);

        //位置
        detailChecker.gameObject.transform.localPosition = Vector2.zero + offset;

        if (intention != null)
        {
            //文本
            detailChecker.SetTextDes(intention.Des);
        }
        else
        {
            string str = "没有意图供显示";
            detailChecker.SetTextDes(str);
        }
    }

    public void PointClickExit()
    {
        Destroy(detailChecker.gameObject);
    }

    public void PointClickHover()
    {
    }

    public void PointClickUp()
    {
    }
}
