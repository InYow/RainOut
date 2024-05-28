using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO 预制件手动赋值，Action的程序化
public class DialogChoiceMenu : MonoBehaviour
{
    public List<DialogChoiceBlock> dialogChoiceBlocks = new();
    public DialogChoiceBlock dialogChoiceBlockPrb;
    public void Init(List<DialogChoiceActionInfo> actioninfos)
    {
        foreach (var info in actioninfos)
        {
            DialogChoiceBlock dialogChoiceBlock = Instantiate(dialogChoiceBlockPrb, transform);
            dialogChoiceBlock.dialogChoiceMenu = this;
            dialogChoiceBlock.Init(info);
        }
    }
    public void Destroy()
    {
        foreach (var item in dialogChoiceBlocks)
        {
            item.Destroy();
        }
        Destroy(this.gameObject);
    }

}
