using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemTextUI : MonoBehaviour
{
    public GameObject textUIPrb;
    private GameObject _textUI;
    private void OnEnable()
    {
        //文字显示UI
        if (_textUI == null)
        {
            _textUI = Instantiate(textUIPrb, transform);
        }
    }
    void Start()
    {
        //文字显示UI
        if (_textUI == null)
        {
            _textUI = Instantiate(textUIPrb, transform);
        }
    }
    private void Update()
    {
    }
    public void SeeText(bool view)
    {
        _textUI.SetActive(view);
    }
    public void SetText(ItemScriptableObject item)
    {
        TextMeshProUGUI tmp = _textUI.GetComponentInChildren<TextMeshProUGUI>();
        if (_textUI != null)
            tmp.text = item.itemName;
    }
}
