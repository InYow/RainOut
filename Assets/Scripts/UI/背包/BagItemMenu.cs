using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//管理BagItemMenuBlock, 作为对外的接口
//USE 使用Prb，Instan的时候Init，传入List<ItemMenuActionInfo> actioninfos
public class BagItemMenu : MonoBehaviour
{
    public List<BagItemMenuBlock> bagItemMenuBlocks = new();
    public BagItemMenuBlock bagItemMenuBlockPrb;
    public void Init(List<ItemMenuActionInfo> actioninfos)
    {
        foreach (var info in actioninfos)
        {
            BagItemMenuBlock bagItemMenuBlock = Instantiate(bagItemMenuBlockPrb, transform);
            bagItemMenuBlock.bagItemMenu = this;
            bagItemMenuBlock.Init(info);
        }
    }
    public void Destroy()
    {
        foreach (var item in bagItemMenuBlocks)
        {
            item.Destroy();
        }
        Destroy(this.gameObject);
    }

}
