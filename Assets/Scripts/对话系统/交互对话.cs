using System.Collections;
using System.Collections.Generic;
using NodeCanvas.DialogueTrees;
using UnityEngine;

public class 交互对话 : MonoBehaviour
{
    public bool Auto;

    private MyInput myInput;

    public DialogueTreeController dialogueTreeController;

    public DialogueActor dialogueActor;

    private void Start()
    {
        myInput = GameObject.FindObjectOfType<MyInput>();

        dialogueTreeController = transform.GetComponentInChildren<DialogueTreeController>();
        if (dialogueTreeController == null)
        {
            Debug.LogError(gameObject.transform.parent.gameObject.name + ": 子物体中没有'DialogueTreeController'");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Auto && other.gameObject.CompareTag("Player"))
        {
            myInput.InterTipEnable(true);
        }

        if (Auto && other.gameObject.CompareTag("Player"))
        {
            Do(other);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!Auto && (Input.GetKeyDown(KeyCode.F) || myInput.inputActions.回合战斗外.交互.triggered))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Do(other);
            }
        }
    }

    private void Do(Collider2D other)
    {
        //Debug.Log("player");

        //Debug.Log("开启对话");
        dialogueTreeController.StartDialogue(dialogueActor);


        #region 设置面朝方向

        对话面朝方向 talkDic = GetComponent<对话面朝方向>();
        if (talkDic != null && talkDic.enabled)
        {
            Vector2 dic = ((Vector2)other.gameObject.transform.position - (Vector2)transform.position).normalized;

            float x = Mathf.Abs(dic.x);
            float y = Mathf.Abs(dic.y);

            if (x > y)
            {
                if (dic.x > 0)
                {
                    talkDic.SetDic(Dic.右);
                }
                else
                {
                    talkDic.SetDic(Dic.左);
                }
            }
            else
            {
                if (dic.y > 0)
                {
                    talkDic.SetDic(Dic.上);
                }
                else
                {
                    talkDic.SetDic(Dic.下);
                }
            }
            #endregion
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Auto && other.gameObject.CompareTag("Player"))
        {
            myInput.InterTipEnable(false);
        }
    }
}
