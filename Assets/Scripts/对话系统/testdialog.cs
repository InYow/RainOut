using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class testdialog : MonoBehaviour
{
    public Story story;
    public static DialogBox Instance;
    public TextMeshProUGUI textGUI;
    public TextMeshProUGUI speaker_NameGUI;
    public Image speaker_Image;
    public GameObject DialogBoxGO;
    private TheNode currentNode;
    public TheNode CurrentNode
    {
        get
        {
            return currentNode;
        }
        set
        {
            currentNode = value;
            if (currentNode.Speaker != null)
            {
                speaker_NameGUI.text = currentNode.Speaker;
            }
            if (currentNode.Emotion != null)
            {
                speaker_Image.sprite = currentNode.Emotion;
            }
            if (currentNode.Text != null)
            {
                textGUI.text = currentNode.Text;
            }
            currentNode.Readed = true;
        }
    }
    private void Start()
    {
        CurrentNode = story.curretNode;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Next();
        }
    }
    public void Next()
    {
        TheNode node = story.NextNode;
        if (node != null)
        {
            //设置信息
            CurrentNode = node;
        }
        else
        {
            //段结束
            List<Choice> choices = story.Choices;
            if (choices != null)
            {
                //打印分支
                Debug.Log("打印分支");
            }
            else
            {
                //分支结束了
                Debug.Log("分支结束了");
            }
        }
    }
}
