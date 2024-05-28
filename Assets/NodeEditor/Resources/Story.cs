using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{
    public PrgTree prgTree;
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
    /// 下个节点，返回null意味当前Paragraph结束了
    /// </summary>
    public TheNode NextNode
    {
        get
        {
            curretNode = prgTree.NextNode(ref nodeIndex, currentParagraph);
            return CurrentNode;
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
    /// 进入下个段落，返回null说明参数不规范
    /// </summary>
    public Paragraph NextParagraph(int index)
    {
        currentParagraph = prgTree.NextParagraph(index, currentParagraph);
        return currentParagraph;
    }
    /// <summary>
    /// 进入下个段落，返回null说明参数不规范
    /// </summary>
    /// <param name="choice"></param>
    /// <returns></returns>
    public Paragraph NextParagraph(Choice choice)
    {
        return prgTree.NextParagraph(choice, currentParagraph);
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
}
