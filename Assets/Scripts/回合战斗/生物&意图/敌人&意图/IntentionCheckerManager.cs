using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntentionCheckerManager : MonoBehaviour
{
    public IntentionChecker intentionCheckerPrb;

    [Tooltip("检视器列表")] public List<IntentionChecker> intentionCheckerList;

    [Tooltip("检视器数量")] public int number;

    private void Start()
    {
    }

    //补全数量
    [ContextMenu("补全数量")]
    public void UpToNumber()
    {
        int i = intentionCheckerList.Count;
        for (; i < number; i++)
        {
            IntentionChecker intentionChecker = Instantiate(intentionCheckerPrb, transform);
            intentionCheckerList.Add(intentionChecker);
        }
    }
}
