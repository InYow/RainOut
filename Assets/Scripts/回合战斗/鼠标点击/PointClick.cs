using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointClick : MonoBehaviour
{
    //悬浮的接口列表
    public List<IPointClickInterface> HoverPoCInterfaceList = new();

    //要在这帧移除的
    public List<IPointClickInterface> HoverToDeleteList = new();

    //按住的接口列表
    public List<IPointClickInterface> PressPoCInterfaceList = new();

    //要在这帧移除的
    public List<IPointClickInterface> PressToDeleteList = new();

    //鼠标当前的屏幕坐标
    Vector3 mousePosition;

    //屏幕坐标转换为世界坐标
    Vector3 worldPosition;

    //从相机到世界坐标的射线
    RaycastHit2D[] hits;

    //当前帧悬浮的接口列表
    List<IPointClickInterface> HoverThisFrameList = new();

    //当前帧按住的接口列表
    List<IPointClickInterface> PressThisFrameList = new();

    // 光标图像
    public Texture2D customCursorTexture;

    // 光标热点
    public Vector2 cursorHotspot = Vector2.zero;

    // 在启动时设置光标
    void Start()
    {
        SetCustomCursor();
    }

    // 设置自定义光标
    void SetCustomCursor()
    {
        Cursor.SetCursor(customCursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // 在游戏中禁用自定义光标
    void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    void Update()
    {
        // 获取鼠标当前的屏幕坐标
        mousePosition = Input.mousePosition;

        // 将屏幕坐标转换为世界坐标
        worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 创建一个从相机到世界坐标的射线
        hits = Physics2D.RaycastAll(worldPosition, Vector2.zero);

        HoverThisFrameList.Clear();
        PressThisFrameList.Clear();

        //检查射线检测结果是否长度为零
        if (hits.Length != 0)
        {
            //遍历检查碰撞到的GameObject
            foreach (var hit in hits)
            {
                // 获取碰撞到的 GameObject
                GameObject GO = hit.collider.gameObject;
                // 检查是否有 IPointClickInterface 接口
                IPointClickInterface pointClickInterface = GO.GetComponent<IPointClickInterface>();
                if (pointClickInterface != null)
                {
                    //判断是否在HoverList中存在
                    if (!HoverPoCInterfaceList.Contains(pointClickInterface))
                    {
                        //添加到HoverList中
                        HoverPoCInterfaceList.Add(pointClickInterface);
                        //调用鼠标进入
                        if (pointClickInterface.Interactable)
                            pointClickInterface.PointClickEnter();
                    }
                    HoverThisFrameList.Add(pointClickInterface);
                }
            }

            //对第一个执行点击判断
            IPointClickInterface firstPointClickInterface = hits[0].collider.gameObject.GetComponent<IPointClickInterface>();
            if (firstPointClickInterface != null)
            {
                //判断是否按下了鼠标
                if (Input.GetMouseButtonDown(0))
                {
                    if (!PressPoCInterfaceList.Contains(firstPointClickInterface))
                    {
                        //添加到PressList中
                        PressPoCInterfaceList.Add(firstPointClickInterface);
                        //调用鼠标按下
                        if (firstPointClickInterface.Interactable)
                            firstPointClickInterface.PointClickDown();
                    }
                }

                //判断鼠标是否按住
                if (Input.GetMouseButton(0))
                {
                    if (!PressThisFrameList.Contains(firstPointClickInterface))
                    {
                        PressThisFrameList.Add(firstPointClickInterface);
                    }
                }

                //判断鼠标是否松开
                if (Input.GetMouseButtonUp(0))
                {
                    if (PressPoCInterfaceList.Contains(firstPointClickInterface))
                    {
                        //调用鼠标松开
                        if (firstPointClickInterface.Interactable)
                            firstPointClickInterface.PointClickUp();
                        //从PressList中移除
                        PressPoCInterfaceList.Remove(firstPointClickInterface);
                    }
                }
            }
        }

        //求依旧悬浮和按住的列表
        HoverToDeleteList = HoverPoCInterfaceList.Except(HoverThisFrameList).ToList();
        PressToDeleteList = PressPoCInterfaceList.Except(PressThisFrameList).ToList();

        //更新 HoverPoCInterfaceList 和 PressPoCInterfaceList
        HoverPoCInterfaceList = HoverThisFrameList.ToList();
        PressPoCInterfaceList = PressThisFrameList.ToList();

        //调用HoverList和PressList中的Hover和Press方法
        foreach (var pointClickInterface in HoverPoCInterfaceList)
        {
            if (pointClickInterface.Interactable)
                pointClickInterface.PointClickHover();
        }
        foreach (var pointClickInterface in PressPoCInterfaceList)
        {
            if (pointClickInterface.Interactable)
                pointClickInterface.PointClick();
        }

        //调用离开方法
        foreach (var pointClickInterface in HoverToDeleteList)
        {
            if (pointClickInterface.Interactable)
                pointClickInterface.PointClickExit();
        }
        foreach (var pointClickInterface in PressToDeleteList)
        {
            if (pointClickInterface.Interactable)
                pointClickInterface.PointClickUp();
        }
    }
}