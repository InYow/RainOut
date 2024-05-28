using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contact_Talk : InterActable
{
    public GameObject PressIcon;//NN
    private void Start()
    {
        if (!PressIcon)
            Debug.Log("PressIcon为空");
        else
            PressIcon.SetActive(false);
    }
    public override Intertype Active()
    {
        //FIN:压入富文本，打开对话框, 定义富文本。
        DialogText dialogText = GetComponent<DialogText>();
        if (dialogText == null)
        {
            Debug.Log($"{gameObject.name}上没有DialogText组件");
            return Intertype.Nullable;
        }
        DialogBox.Instance.Open(dialogText.Data);
        return Intertype.Contact_Talk;
    }
    public override void CheckEnter()
    {
        PressIcon.SetActive(true);
        base.CheckEnter();
    }
    public override void CheckStay()
    {
        base.CheckStay();
    }
    public override void CheckExit()
    {
        PressIcon.SetActive(false);
        base.CheckExit();
    }
}
