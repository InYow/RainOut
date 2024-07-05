using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour, IPointClickInterface
{
    public SpriteRenderer spriteRenderer;

    public Sprite sprite;

    public string str;

    [Tooltip("文本框")] public DetailChecker detailCheckerPrb;

    [Header("PointClick和detailChecker")]
    public bool interactable;

    public DetailChecker detailChecker;

    public bool Interactable { get => interactable; set => interactable = value; }

    public Vector2 offset;

    public void SetIntention(Intention intention)
    {
        spriteRenderer.sprite = intention.sprite;
    }

    public void PointClickEnter()
    {
        //实例化
        detailChecker = Instantiate(detailCheckerPrb, transform);

        //位置
        detailChecker.gameObject.transform.localPosition = Vector2.zero + offset;

        Text();
    }

    public virtual void Text()
    {
        //文本
        detailChecker.SetTextDes(str);
    }

    public void PointClickExit()
    {
        Destroy(detailChecker.gameObject);
    }

    private void Start()
    {
    }

    [ContextMenu("INIT")]
    public void Init()
    {
        spriteRenderer.sprite = sprite;
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
