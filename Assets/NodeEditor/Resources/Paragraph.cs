using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[Serializable]
public class Paragraph : ScriptableObject
{
    [HideInInspector] public PrgViewer prgViewer;
    public Vector2 position;
    public string prg_Name;//名称
    public List<TheNode> nodes = new();//包含的节点
    public List<Paragraph> inPrgs = new();//输入的段落
    public List<Choice> outChcs = new();//输出的段落
    /// <summary>
    /// 当前节点
    /// </summary>
    public TheNode CurrentNode(int NodeIndex)
    {
        if (NodeIndex > nodes.Count - 1)
        {
            return null;
        }
        return nodes[NodeIndex];
    }
    /// <summary>
    /// 下个节点
    /// </summary>
    public TheNode NextNode(ref int NodeIndex)
    {
        NodeIndex++;
        if (NodeIndex >= nodes.Count)
        {
            //溢出数量了
            NodeIndex = nodes.Count - 1;
            //Debug.Log("nul Paragraph");
            return null;
        }
        else
        {
            //Debug.Log(nodes[NodeIndex].name);
            return nodes[NodeIndex];
        }

    }
    /// <summary>
    /// 输出选项
    /// </summary>
    public List<Choice> Choices
    {
        get
        {
            return outChcs;
        }
    }
    public void AddInPrg(Paragraph prg)
    {
        inPrgs.Add(prg);
    }
    public void AddOutPrg(Choice chc)
    {
        outChcs.Add(chc);
    }

    internal void RemoveInPrg(Paragraph prg)
    {
        inPrgs.Remove(prg);
    }
    public Choice FindChoice(Paragraph prg)
    {
        Choice chc = null;
        foreach (var item in outChcs)
        {
            if (item.prg == prg)
            {
                chc = item;
                break;
            }
        }
        return chc;
    }
    internal void RemoveOutPrg(Paragraph prg)
    {
        Choice chc = FindChoice(prg);
        if (chc != null)
        {
            outChcs.Remove(chc);
            AssetDatabase.RemoveObjectFromAsset(chc);
            Undo.DestroyObjectImmediate(chc);
            AssetDatabase.SaveAssets();
        }
    }
}