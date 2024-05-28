using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO 预制件手动赋值，Action的程序化
//管理DialogChoiceBlock, 作为对外的接口
//USE 使用Prb，Instan的时候Init，传入List<DialogChoiceActionInfo> actioninfos
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
