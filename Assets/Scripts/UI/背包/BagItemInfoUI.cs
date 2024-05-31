using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BagItemInfoUI : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Vector2 offset;
    private void Start()
    {
        SetActive(false);
    }
    //设置可见
    public void SetActive(bool view)
    {
        gameObject.SetActive(view);
    }
    public void SetPosition(Vector2 vector2)
    {
        //        Debug.Log("设置位置");
        transform.position = vector2;
    }
    //设置文本内容
    public int SetText(ItemData data)
    {
        if (data == null)
            return 1;
        textMeshPro.text = $"{data.itemName}\n----\n介绍\n----\n{data.description}\n----\n标签\n----\n{data.tag}";
        return 0;
    }
}
