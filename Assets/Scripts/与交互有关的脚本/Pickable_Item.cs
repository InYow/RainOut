using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable_Item : InterActable
{
    public override Intertype Active()
    {
        return Intertype.Pickable_Item;
    }
    public override void CheckEnter()
    {
        Item item = GetComponent<Item>();
        item.SeeText(true);
        base.CheckEnter();
    }
    public override void CheckStay()
    {
        base.CheckStay();
    }
    public override void CheckExit()
    {
        Item item = GetComponent<Item>();
        item.SeeText(false);
        base.CheckExit();
    }
}
