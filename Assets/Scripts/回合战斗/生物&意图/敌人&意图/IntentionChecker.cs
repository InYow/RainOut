using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntentionChecker : MonoBehaviour, IPointClickInterface
{
    public Intention intention;

    public bool interactable;

    public bool Interactable { get => interactable; set => interactable = value; }

    [Tooltip("文本框")] public DetailChecker detailCheckerPrb;

    public DetailChecker detailChecker;

    public Vector2 offset;

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
        detailChecker.gameObject.transform.localPosition = Vector2.zero;
        //文本
        detailChecker.SetTextDes(intention.Des);
    }

    public void PointClickExit()
    {
        Destroy(detailChecker.gameObject);
    }

    public void PointClickHover()
    {
        throw new System.NotImplementedException();
    }

    public void PointClickUp()
    {
        throw new System.NotImplementedException();
    }
}
