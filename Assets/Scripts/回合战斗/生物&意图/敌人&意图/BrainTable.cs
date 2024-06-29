using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
[CreateAssetMenu(fileName = "New BrainTable", menuName = "ScriptableObjects/Intention/BrainTable", order = 1)]


public class BrainTable : ScriptableObject
{
    public List<Intention> intention;

    //概率
    public List<int> percentList;
}
