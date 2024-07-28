using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//功能 解释Story脚本
//使用方法 public Story Story = 要解释的脚本 OpenOrClose打开或关闭
public class DialogUI : MonoBehaviour
{
    [Header("目前是只能同时有一个DialogUI")]
    public static DialogUI Instance;
    [Header("绑定的对话组件")]
    public Story story;
    public Story Story
    {
        get
        {
            return story;
        }
        set
        {
            story = value;
            CurrentNode = Story.CurrentNode;
        }
    }
    [Header("选项预制件")]
    public DialogChoiceMenu dialogChoiceMenuPrb;
    [Header("当前选项菜单")]
    public DialogChoiceMenu dialogChoiceMenu;
    [Header("UI物体")]
    public GameObject DialogUIGO;
    public TextMeshProUGUI textGUI;
    public TextMeshProUGUI speaker_NameGUI;
    public Image speaker_Image;
    [Header("拓展属性")]
    public bool haveClose = false;
    [Header("当前UI中的数据")]
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
            if (CurrentNode != null)
            {
                if (CurrentNode.Speaker != "")
                {
                    speaker_NameGUI.text = CurrentNode.Speaker;
                }
                if (CurrentNode.Emotion != null)
                {
                    speaker_Image.sprite = CurrentNode.Emotion;
                }
                if (CurrentNode.Text != "")
                {
                    if (CurrentNode.Readed)
                    {
                        textGUI.text = "<#00FFBC>" + CurrentNode.Text;
                    }
                    else
                    {
                        textGUI.text = CurrentNode.Text;
                    }
                }
                CurrentNode.Readed = true;
            }
            //设置的节点为空
            else
            {
                Debug.Log("设置的节点为空");
            }
        }
    }
    private void Awake()
    {
        Instance = this;
    }
    public void OpenOrClose()
    {
        DialogUIGO.SetActive(!DialogUIGO.activeSelf);
        if (DialogUIGO.activeSelf == true)
        {
            OnOpen();
        }
    }
    private void Update()
    {
        if (this.dialogChoiceMenu == null)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                OpenOrClose();
            }
            //对话UIGO处于打开状态
            if (DialogUIGO.activeSelf)
            {
                if (Input.GetKeyDown(KeyCode.L))
                {
                    Next();
                }
            }
        }
    }
    /// <summary>
    /// 在当前节点中进行下一个节点
    /// </summary>
    public void Next()
    {
        TheNode node = Story.NextNode;
        if (node != null)
        {
            //Debug.Log("nul Paragraph");
            //节点设置信息
            CurrentNode = node;
        }
        else
        {
            //Debug.Log(node.name);
            //段结束
            Fin();
        }
    }
    /// <summary>
    /// 当前段落结束
    /// </summary>
    private void Fin()
    {
        //处理当前段的结束效果
        List<Choice> choices = Story.Choices;
        switch (Story.currentParagraph.extention)
        {
            case Paragraph.Paragraph_Extention.normal:
                {
                    break;
                }
            case Paragraph.Paragraph_Extention.close:
                {
                    Choice choice = null;
                    foreach (var item in choices)
                    {
                        if (item.Text == "" && !item.locked)
                        {
                            //支持自动跳转的选项
                            //FIXME 上锁选项
                            choice = item;
                            break;
                        }
                    }
                    if (choice == null)
                    {
                        //没有符合跳转的选项, 则重新开始自身
                        //关闭
                        DialogUIGO.SetActive(false);
                        //记录跳转属性
                        haveClose = true;
                        return;
                    }
                    else
                    {
                        //无限次
                        if (Story.CurrentParagraph.repeat_Times == -1)
                        {
                            //关闭
                            DialogUIGO.SetActive(false);
                            //记录跳转属性
                            haveClose = true;
                            return;
                        }
                        //有限次
                        else if (Story.CurrentParagraph.repeat_Times > 0)
                        {
                            Story.CurrentParagraph.repeat_Times--;
                            //关闭
                            DialogUIGO.SetActive(false);
                            //记录跳转属性
                            haveClose = true;
                            return;
                        }
                        //repeat_Times > 0 但是 默认无限次重复
                        else if (choices.Count == 1)
                        {
                            //关闭
                            DialogUIGO.SetActive(false);
                            //记录跳转属性
                            haveClose = true;
                            return;
                        }
                        //不再触发
                        else
                        {
                            break;
                        }
                    }
                }
            default:
                {
                    break;
                }
        }
        if (choices != null)
        {
            //如果 choices只有一个且choice.text == null
            //则自动跳转
            if (choices.Count == 1 && choices[0].Text == "")
            {
                Choose(choices[0]);
                return;
            }
            // 打印分支
            //Debug.Log("打印分支");
            DialogChoiceMenu dialogChoiceMenu = Instantiate(dialogChoiceMenuPrb, transform);
            this.dialogChoiceMenu = dialogChoiceMenu;
            List<DialogChoiceActionInfo> dialogChoiceActionInfos = new();
            foreach (var choice in choices)
            {
                var capturedChoice = choice;
                DialogChoiceActionInfo d = new(choice.Text, capturedChoice, () => Choose(capturedChoice));
                dialogChoiceActionInfos.Add(d);
            }
            dialogChoiceMenu.Init(dialogChoiceActionInfos);
        }
        else
        {
            //分支结束了
            Debug.Log("分支结束了");
        }

    }

    /// <summary>
    /// 进入选择的路径
    /// </summary>
    /// <param name="choice"></param>
    public void Choose(Choice choice)
    {
        Paragraph paragraph = Story.NextParagraph(choice);
        if (paragraph == null)
        {
            //参数不规范
            Debug.Log("参数不规范 story.NextParagraph(choice);");
        }
        if (dialogChoiceMenu != null)
        {
            dialogChoiceMenu.Destroy();
        }
        TheNode node = Story.CurrentNode;
        if (node == null)
        {
            //空段落，或者说段落结束
            Fin();
            Debug.Log("空段落，或者说段落结束");
        }
        //加载当前段落
        CurrentNode = node;
    }
    public void OnOpen()
    {
        //检查本次打开是否为关闭后的打开
        if (haveClose)
        {
            haveClose = false;
            //关闭 close 后打开的处理
            //自动跳转
            List<Choice> choices = Story.Choices;
            Choice choice = null;
            foreach (var item in choices)
            {
                if (item.Text == "")
                {
                    //支持自动跳转的选项
                    choice = item;
                    break;
                }
            }
            if (choice == null)
            {
                //没有符合跳转的选项, 则重新开始自身
                Story.ReStartCurrentParagraph();
                CurrentNode = CurrentNode;
                return;
            }
            else
            {
                Choose(choice);
            }
        }
    }
}