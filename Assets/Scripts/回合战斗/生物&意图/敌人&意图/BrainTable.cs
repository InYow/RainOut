using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[CreateAssetMenu(fileName = "New BrainTable", menuName = "ScriptableObjects/Intention/BrainTable", order = 1)]


public class BrainTable : ScriptableObject
{
    //所有可选意图
    public List<Intention> intentionList;

    //概率
    public List<int> percentList;

    public Intention RandomIntention()
    {
        //计算总权重
        int percent_Sum = 0;
        foreach (var p in percentList)
        {
            percent_Sum += p;
        }

        //随机数
        int percent = Random.Range(1, percent_Sum + 1);
        int percent_cul = 0;

        //计算索引 i
        int i = 0;
        foreach (var p in percentList)
        {
            percent_cul += p;
            if (percent <= percent_cul)
            {
                break;
            }
            i++;
        }

        //返回意图
        if (i >= intentionList.Count)
        {
            throw new System.NotImplementedException("可选意图与概率不匹配 from " + this.name);
        }
        return intentionList[i];
    }
}
