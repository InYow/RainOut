using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dic
{
    上,
    下,
    左,
    右
}

public class 对话面朝方向 : MonoBehaviour
{

    [SerializeField]
    private Dic dic;

    private SpriteRenderer spriteRenderer;

    public Sprite 向上看精灵图;

    public Sprite 向下看精灵图;

    public Sprite 向左看精灵图;

    public Sprite 向右看精灵图;

    public void SetDic(Dic d)
    {
        dic = d;

        switch (d)
        {

            case Dic.上:
                {
                    spriteRenderer.sprite = 向上看精灵图;
                    break;
                }

            case Dic.下:
                {
                    spriteRenderer.sprite = 向下看精灵图;
                    break;
                }

            case Dic.左:
                {
                    spriteRenderer.sprite = 向左看精灵图;
                    break;
                }

            case Dic.右:
                {
                    spriteRenderer.sprite = 向右看精灵图;
                    break;
                }

            default:
                break;
        }

    }


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        }
        SetDic(dic);
    }
}
