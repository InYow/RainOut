using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HurtValueFlow : MonoBehaviour
{
    public string text;

    public TextMeshProUGUI textGUI;

    private void OnValidate()
    {
        textGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        this.text = text;

        textGUI.text = this.text;
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
