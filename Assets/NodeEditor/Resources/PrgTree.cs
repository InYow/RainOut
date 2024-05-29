using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu()]
public class PrgTree : ScriptableObject
{
    public List<Paragraph> prgList = new();//所有的段落
    //private Paragraph currentPrg;
    /// <summary>
    /// 当前节点
    /// </summary>
    public TheNode CurrentNode(int NodeIndex, Paragraph CurrentParagraph)
    {
        return CurrentParagraph.CurrentNode(NodeIndex);
    }
    /// <summary>
    /// 下个节点，返回null意味当前Paragraph结束了
    /// </summary>
    public TheNode NextNode(ref int NodeIndex, Paragraph CurrentParagraph)
    {
        TheNode theNode = CurrentParagraph.NextNode(ref NodeIndex);
        if (theNode == null)
        {
            //Debug.Log("nul Paragraph");
            return null;
        }
        else
        {
            //Debug.Log(theNode.name);
            return theNode;
        }

    }
    /// <summary>
    /// 进入下个段落，返回null说明参数不规范
    /// </summary>
    public Paragraph NextParagraph(int index, Paragraph CurrentParagraph)
    {
        if (index >= 0 && index < Choices(CurrentParagraph).Count)
        {
            CurrentParagraph = Choices(CurrentParagraph)[index].prg;
            return CurrentParagraph;
        }
        else
        {
            return null;
        }
    }
    /// <summary>
    /// 进入下个段落，返回null说明参数不规范
    /// </summary>
    /// <param name="choice"></param>
    /// <returns></returns>
    public Paragraph NextParagraph(Choice choice, Paragraph CurrentParagraph)
    {
        if (choice != null && Choices(CurrentParagraph).Contains(choice))
        {
            CurrentParagraph = choice.prg;
            return CurrentParagraph;
        }
        {
            return null;
        }
    }
    /// <summary>
    ///  如果Count为0，说明当前分支结束了
    /// </summary>
    public List<Choice> Choices(Paragraph CurrentParagraph)
    {
        return CurrentParagraph.Choices;
    }
#if UNITY_EDITOR
    public Paragraph CreatePrg()
    {
        Paragraph prg = ScriptableObject.CreateInstance(typeof(Paragraph)) as Paragraph;
        prg.name = (prgList.Count + 1).ToString();

        prg.prg_Name = (prgList.Count + 1).ToString();
        prgList.Add(prg);
        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(prg, this);
        }
        AssetDatabase.SaveAssets();
        return prg;
    }
    public Paragraph DeletePrg(Paragraph prg)
    {
        prgList.Remove(prg);
        AssetDatabase.RemoveObjectFromAsset(prg);
        Undo.DestroyObjectImmediate(prg);
        AssetDatabase.SaveAssets();
        return prg;
    }
    public void RemovePrgConnection(Paragraph outPrg, Paragraph inPrg)
    {
        outPrg.RemoveOutPrg(inPrg);
        inPrg.RemoveInPrg(outPrg);
    }
    public Choice AddChild(Paragraph outPrg, Paragraph inPrg, string choice)
    {
        Choice chc = ScriptableObject.CreateInstance(typeof(Choice)) as Choice;
        chc.name = $"C {outPrg.name}->{inPrg.name}";
        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(chc, outPrg);
        }
        AssetDatabase.SaveAssets();
        chc.prg = inPrg;
        chc.Text = choice;
        //
        outPrg.AddOutPrg(chc);
        inPrg.AddInPrg(outPrg);
        return chc;
    }

#endif
}
