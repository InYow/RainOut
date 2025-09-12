using System.Collections;
using System.Collections.Generic;
using NodeCanvas.Tasks.Actions;
using UnityEngine;

public class 场景传送点 : MonoBehaviour
{

    public string 名称;

    public Transform 传送地点;

    [Header("目标地")]

    public string 要加载的场景;

    public string 目标传送点;

    private GameObject 传送物品;

    public void 传送(/*GameObject 物品*/)
    {
        Test_Find();
        Test_SendMessage();

        //场景切换管理类.LoadScene(要加载的场景, 目标传送点, 物品);
    }







    [ContextMenu("传送")]
    public void Test_SendMessage()
    {
        场景切换管理类.LoadScene(要加载的场景, 目标传送点, 传送物品);
    }

    [ContextMenu("查找")]
    public void Test_Find()
    {
        传送物品 = FindObjectOfType<玩家移动>().gameObject;
    }

}
