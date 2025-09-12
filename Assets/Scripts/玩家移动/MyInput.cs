using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInput : MonoBehaviour
{
    public 玩家输入 inputActions;

    public static MyInput instance;

    public GameObject 交互提示;

    public bool enable;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

            inputActions = new();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        if (enable)
        {
            InputEnable();
        }
        else
        {
            InputDisable();
        }
    }

    private void OnEnable()
    {
        InputEnable();
    }

    private void OnDisable()
    {
        InputDisable();
    }

    public void InputEnable()
    {
        inputActions?.Enable();
    }

    public void InputDisable()
    {
        inputActions?.Disable();
    }

    public void InterTipEnable(bool b)
    {
        交互提示.SetActive(b);
    }

}
