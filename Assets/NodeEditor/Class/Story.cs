using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    [Header("使用的对话数据 必须赋值")]
    public PrgTree prgTree;
    [Header("")]
    [Header("当前节点,段落的信息 可以不用管")]
    public int nodeIndex;
    public TheNode curretNode;
    public Paragraph currentParagraph;
    public int NodeIndex
    {
        get
        {
            return nodeIndex;
        }
        set
        {
            nodeIndex = value;
        }
    }
    private void Awake()
    {
        currentParagraph = prgTree.prgList[0];
        curretNode = prgTree.CurrentNode(NodeIndex, currentParagraph);
        NodeIndex = 0;
    }
    /// <summary>
    /// 当前节点，null意味无节点或超出索引
    /// </summary>
    public TheNode CurrentNode
    {
        get
        {
            if (NodeIndex == -1)
            {
                NodeIndex = 0;
            }
            curretNode = prgTree.CurrentNode(NodeIndex, currentParagraph);
            return curretNode;
        }
    }
    /// <summary>
    /// 下个节点，null意味当前Paragraph结束了
    /// </summary>
    public TheNode NextNode
    {
        get
        {
            curretNode = prgTree.NextNode(ref nodeIndex, currentParagraph);
            //            Debug.Log(nodeIndex);
            return curretNode;
        }
    }
    /// <summary>
    /// 当前段落
    /// </summary>
    public Paragraph CurrentParagraph
    {
        get
        {
            return currentParagraph;
        }
    }
    /// <summary>
    /// 下个段落，null说明参数不规范
    /// </summary>
    public Paragraph NextParagraph(int index)
    {
        currentParagraph = prgTree.NextParagraph(index, currentParagraph);
        nodeIndex = 0;
        return currentParagraph;
    }
    /// <summary>
    /// 下个段落，null说明参数不规范
    /// </summary>
    /// <param name="choice"></param>
    /// <returns></returns>
    public Paragraph NextParagraph(Choice choice)
    {
        currentParagraph = prgTree.NextParagraph(choice, currentParagraph);
        nodeIndex = 0;
        return currentParagraph;
    }
    /// <summary>
    ///  如果Count为0，说明当前分支结束了
    /// </summary>
    public List<Choice> Choices
    {
        get
        {
            return prgTree.Choices(currentParagraph);
        }
    }
    /// <summary>
    /// 重新开始当前段落
    /// </summary>
    public void ReStartCurrentParagraph()
    {
        NodeIndex = 0;
    }
}
