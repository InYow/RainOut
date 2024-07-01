using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    [Tooltip("生物体")] public Entity entity;

    [Tooltip("脑子模板")] public BrainTable brainTable;

    [Tooltip("旗下检视器")] public IntentionCheckerManager intentionCheckerManager;

    private void OnValidate()
    {
        if (entity == null)
        {
            entity = GetComponent<Entity>();
        }
    }
}
