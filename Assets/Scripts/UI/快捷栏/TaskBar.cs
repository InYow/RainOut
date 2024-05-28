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
            //之前的设置为不选中 
            if (m_selectBlock != null)
            {
                m_selectBlock.Selected = false;
            }
            this.m_selectBlock = value;
            //现在的设置为选中
            m_selectBlock.Selected = true;
            this.CarryOn(value);
        }
    }
    public InterActive m_interActive;
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
    private void CarryOn(TaskBlock taskBlock)
    {
        hand.CarryOn(taskBlock.signedBagBlock);
    }
}
