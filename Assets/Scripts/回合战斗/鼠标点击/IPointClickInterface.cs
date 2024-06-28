using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPointClickInterface
{
    //是否可以使用
    public bool Interactable { get; set; }

    //鼠标进入时触发
    public void PointClickEnter();

    //鼠标悬浮hover时触发
    public void PointClickHover();

    //鼠标离开时触发
    public void PointClickExit();

    //鼠标点击时触发
    public void PointClickDown();

    //鼠标按住时触发
    public void PointClick();

    //鼠标松开时触发
    public void PointClickUp();

}
