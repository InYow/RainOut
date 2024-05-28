using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClickBag : MonoBehaviour
{
    [Header("检测有效层")]
    public LayerMask itemLayer;
    public BagItemInfoUI itemInfoUI;
    public BaseBlock m_selectedBlock;
    private void Start()
    {
        m_selectedBlock = null;
    }
    void Update()
    {
        // 检测鼠标位置的UI
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, itemLayer);
        // 鼠标指中了格子
        //FIXME 背包》选中检测为通用碰撞，指定UI层
        if (hit.collider != null)
        {
            // 悬浮的Collider
            Collider2D Collider = hit.collider;
            Debug.Log($"悬浮在{Collider.gameObject}上面");
            #region 背包格子BagBlock
            BaseBlock bagBlock = Collider.gameObject.GetComponent<BaseBlock>();
            if (bagBlock != null)
            {
                // 在这里处理背包格子BagBlock的相关逻辑
                // 默认悬浮
                // 显示悬浮在格子上
                //物品文本--UI设置可见，设置位置，传递信息
                if (!bagBlock.Empty())
                {
                    //itemInfoUI.SeeView(true);
                    Vector3 vector3 = Camera.main.ScreenToWorldPoint(Input.mousePosition); vector3.z = itemInfoUI.gameObject.transform.position.z; itemInfoUI.gameObject.transform.position = vector3 + (Vector3)itemInfoUI.offset;
                    //itemInfoUI.SetText(bagBlock.m_item.data);
                }
                // 右击
                if (Input.GetMouseButtonDown(1))
                {
                    bagBlock.Pop();
                }
                //FIN 实现拖动改变登记位置。
                if (Input.GetMouseButtonDown(0))
                {
                    // 左击
                    //bagBlock.OnClick(this);
                    // FIN 调用BaseBlock对应的函数Click()，将相关字段传入
                    // FIN 将下面的整理到BagBlock中
                }
            }
        }
        //没有指中格子
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //清空上次选择
                m_selectedBlock = null;
            }
        }
        #endregion
    }
}