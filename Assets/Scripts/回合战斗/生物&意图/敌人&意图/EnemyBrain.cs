using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Entity entity;
    private void OnValidate()
    {
        if (entity == null)
        {
            entity = GetComponent<Entity>();
        }
    }
}
