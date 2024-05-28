using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Bag : MonoBehaviour
{
    //FIN:背包功能
    //1 物品的存在 Item类 使用引用+组合
    //2 存放物品
    //3 取出物品
    //4 管理物品
    //5 操作界面
    // [Header("滚动面板")]
    // public Transform contentTrans;
    // [Header("背包容量")]
    // public int capacity;
    // public BagBlock blockPrb;
    [Header("背包格子队列")]
    public List<BagBlock> blockList = new();
    [Header("物品介绍")]
    public BagItemInfoUI Info;
    private void Start()
    {
        //        Debug.Log(blockList.Count);
        Init();
    }
    //FIN 写一个快捷操作栏，映射背包。点击操作栏的格子，切换手中的东西。
    //FIN 重构ItemData，添加Icon
    void Init()
    {
        foreach (var bagBlock in blockList)
        {
            bagBlock.bag = this;
        }
    }
    //获取空位
    public BagBlock GetEmpty()
    {
        foreach (var block in blockList)
        {
            if (block.Empty())
            {
                return block;
            }
        }
        Debug.Log($"{gameObject}背包满了");
        return null;
    }
    //存放
    public void Add(Item item)
    {
        if (item == null)
        {
            Debug.Log($"试图将null放入背包 from{gameObject}");
            return;
        }
        BagBlock block = GetEmpty();
        if (block == null)
        {
            return;
        }
        block.Push(item);
    }
    public void Add(GameObject gameObject)
    {
        var item = gameObject.GetComponent<Item>();
        Add(item);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
    internal void OpenOrClose()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
