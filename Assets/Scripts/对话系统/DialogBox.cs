using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogBox : MonoBehaviour
{
    public static DialogBox Instance;
    public TextMeshProUGUI textGUI;
    public TextMeshProUGUI speaker_NameGUI;
    public Image speaker_Image;
    public GameObject DialogBoxGO;
    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void Next()
    {
        if (gameObject.activeSelf)
            if (textGUI.pageToDisplay < textGUI.GetTextInfo(textGUI.text).pageCount)
            {
                textGUI.pageToDisplay++;
            }
            else
            {
                Close();
            }
    }
    public void Open(DialogTextScriptable textData)
    {
        textGUI.pageToDisplay = 1;
        textGUI.text = textData.text;
        speaker_NameGUI.text = textData.speaker_Name;
        speaker_Image.sprite = textData.speaker_Image;
        DialogBoxGO.SetActive(true);
    }
    public void Close()
    {
        DialogBoxGO.SetActive(false);
    }
}
