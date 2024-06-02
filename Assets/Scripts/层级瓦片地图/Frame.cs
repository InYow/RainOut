using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public int frame;
    void Start()
    {
        Application.targetFrameRate = frame;
    }

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = frame;
    }
}
