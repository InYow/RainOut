using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//初始化选中格子，颜色标记当前手持格子
public class TaskBar : MonoBehaviour
{
    [Header("快捷栏格子队列")]
    public Hand hand;
    public List<TaskBlock> m_blockList = new();//格子列表
    public TaskBlock m_selectBlock;
    public TaskBlock SelectedBlock
    {
        get
        {
            return this.m_selectBlock;
        }
        set
        {
            //Debug.Log(" SelectedBlock TaskBar");
            //更新到手上
            hand.CarryBagBlock = value.linkBagBlock;
            //之前的设置为不选中 
            if (SelectedBlock != null)
            {
                SelectedBlock.Selected = false;
            }
            //字段赋值
            this.m_selectBlock = value;
            //现在的设置选中
            SelectedBlock.Selected = true;
        }
    }
    private void Start()
    {
        foreach (var block in m_blockList)
        {
            block.taskBar = this;
        }
        if (SelectedBlock == null && m_blockList.Count != 0)
        {
            SelectedBlock = m_blockList[0];
        }
    }
}
