using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//展示图标 悬浮文本
public class Checker : MonoBehaviour, IPointClickInterface
{
    //图标
    public Sprite sprite;

    //文本
    public string str;


    [Header("依附组件")]
    public SpriteRenderer spriteRenderer;

    [Tooltip("文本框")] public DetailChecker detailCheckerPrb;

    public bool interactable;

    public bool Interactable { get => interactable; set => interactable = value; }

    public DetailChecker detailChecker;

    [Tooltip("文本框位置")] public Vector2 offset;


    //图标
    public void Sprite()
    {
        spriteRenderer.sprite = sprite;
    }

    //文本
    public virtual void Text()
    {
        detailChecker.SetTextDes(str);
    }

    public void PointClickEnter()
    {
        //实例化
        detailChecker = Instantiate(detailCheckerPrb, transform);

        //位置
        detailChecker.gameObject.transform.localPosition = Vector2.zero + offset;

        Text();
    }

    public void PointClickExit()
    {
        Destroy(detailChecker.gameObject);
    }

    public void PointClick()
    {
    }

    public void PointClickDown()
    {
    }

    public void PointClickHover()
    {
    }

    public void PointClickUp()
    {
    }
}
