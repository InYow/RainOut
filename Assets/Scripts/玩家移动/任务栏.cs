using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 任务栏 : MonoBehaviour
{
    public GameObject 任务栏GO;

    private MyInput myInput;

    private void Update()
    {
        if (myInput.inputActions.回合战斗外.任务栏.triggered)
        {
            任务栏GO.SetActive(!任务栏GO.activeSelf);
        }
    }

    private void Awake()
    {
        myInput = GameObject.FindObjectOfType<MyInput>();
    }
}
