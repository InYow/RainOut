using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;

//AddUILayer(UILayer uilayer) 添加UILAYER
//RemoveTail() 关闭最上层UILAYER
//ClearList() 关闭所有UILAYER
//RemoveFrom(int index) 关闭从index之后UILAYER
public class UIManager : MonoBehaviour
{
    public static UIManager uIManager;

    public List<UILayer> uilayerList;

    public virtual void Awake()
    {
        if (uIManager == null)
        {
            uIManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //类内使用
    //添加到List
    //打开添加的uilayer
    private static void AddToList(UILayer uILayer)
    {
        uIManager.uilayerList.Add(uILayer);
        uILayer.Open();
    }

    //类内使用
    //List移除
    //关闭添加的uilayer
    private static void RemoveAtList(int index)
    {
        uIManager.uilayerList[index].Close();
        uIManager.uilayerList.RemoveAt(index);
    }

    //自动调整uilayerList以匹配传入参数uilayer的sort
    /// <summary>
    /// 添加uilayer
    /// </summary>
    /// <param name="uilayer"></param>
    /// <exception cref="NotImplementedException">层数大于最后一位+1，出错了</exception>
    public static void AddUILayer(UILayer uilayer)
    {
        //判断是否已经在队列中
        if (uIManager.uilayerList.Contains(uilayer))
        {
            return;
        }
        else
        {
            //判断是否为空
            if (uIManager.uilayerList.Count == 0)
            {
                //添加进去
                AddToList(uilayer);
            }
            else
            {
                //倒数开始判断layer大小
                int i = 1;
                int layer = uilayer.sort;

                //层数大于最后一位+1，出错了
                if (layer > uIManager.uilayerList[^i].sort + 1)
                {
                    throw new NotImplementedException("层数大于最后一位+1，出错了");
                }

                //等于最后一位+1,恰好是
                else if (layer == uIManager.uilayerList[^i].sort + 1)
                {
                    //添加进去
                    AddToList(uilayer);
                }

                //小于最后一位+1,往前剥
                else
                {
                    int index = -1;
                    for (; layer != uIManager.uilayerList[^i].sort + 1; i++)
                    {
                        if (i == uIManager.uilayerList.Count)
                        {
                            index = -2;
                            break;
                        }
                    }
                    if (index == -1)
                    {
                        //有
                        index = uIManager.uilayerList.Count - i;
                    }
                    else
                    {
                        //剥到头了也没有
                        index = -1;
                    }

                    //到这里，index==-1则没有，否则index就指向要跟随的UILayer.
                    if (index == -1)
                    {
                        //从尾移除所有的uilayer
                        ClearList();
                        AddToList(uilayer);
                    }
                    else
                    {
                        //从尾移除
                        RemoveFrom(index);
                        AddToList(uilayer);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 移除末尾的uilayer
    /// </summary>
    public static void RemoveTail()
    {
        RemoveAtList(uIManager.uilayerList.Count - 1);
    }

    /// <summary>
    /// 从尾移除所有的uilayer
    /// </summary>
    public static void ClearList()
    {
        //从尾移除所有的uilayer
        while (uIManager.uilayerList.Count != 0)
        {
            RemoveAtList(uIManager.uilayerList.Count - 1);
        }
    }

    /// <summary>
    /// index之后的移除掉 (不包括index) , 从尾移除. 
    /// </summary>
    /// <param name="index"></param>
    public static void RemoveFrom(int index)
    {
        while (uIManager.uilayerList.Count > index + 1)
        {
            RemoveAtList(uIManager.uilayerList.Count - 1);
        }
    }
}
