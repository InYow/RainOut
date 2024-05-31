using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//管理DialogChoiceBlock, 提供对外接口
//使用方法 使用Prb，Instan的时候Init，传入List<DialogChoiceActionInfo> actioninfos
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
