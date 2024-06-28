using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILayer : MonoBehaviour
{
    //排序层级
    public int sort;

    //指定的物品
    public GameObject GO;

    private void OnValidate()
    {
        GO = gameObject;
    }

    public virtual void AddToManager()
    {
        UIManager.AddUILayer(this);
    }

    public virtual void Open()
    {
        GO.SetActive(true);
    }

    public virtual void Close()
    {
        GO.SetActive(false);
    }

    public virtual void Change()
    {

    }
}
