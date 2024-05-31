using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class BagBlock : BaseBlock, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    //F鼠标悬浮时显示浮窗，离开消失
    //右键打开操作栏
    //F左键拖拽、虚影跟随、松开交换位置
    private InputControllor player;
    public Bag bag;
    public List<TaskBlock> LinkTaskBlocks;
    public Item item;
    public Item item1;//只用一次。要付给item的值
    public Item Item
    {
        get
        {
            return item;
        }
        set
        {
            item1 = value;
            if (value == null)
            {
                m_itemIcon.sprite = m_iconNone;
                if (LinkTaskBlocks.Count != 0)
                {
                    for (int i = 0; i < LinkTaskBlocks.Count; i++)
                    {
                        //Debug.Log("value == null" + "BagBlock");
                        LinkTaskBlocks[i].m_itemIcon.sprite = m_iconNone;
                        LinkTaskBlocks[i].OnLinkBagBlockItemAction?.Invoke();
                    }
                }
            }
            else
            {
                m_itemIcon.sprite = value.data.icon;
                if (LinkTaskBlocks.Count != 0)
                {
                    for (int i = 0; i < LinkTaskBlocks.Count; i++)
                    {
                        //Debug.Log("value != null" + "BagBlock");
                        LinkTaskBlocks[i].m_itemIcon.sprite = value.data.icon;
                        LinkTaskBlocks[i].OnLinkBagBlockItemAction?.Invoke();
                    }
                }
            }
            item = value;
        }
    }
    [Header("物品右键操作栏")]
    public BagItemMenu bagItemMenuPrb;
    [Header("虚影")]
    public GameObject fadeIconPrb;
    public GameObject fadeIcon;
    public RectTransform fade_rectTrans;
    private RectTransform rectTransform;
    //private Canvas canvas;
    //private CanvasGroup canvasGroup;
    //初始继承
    void Start()
    {
        Init();
    }
    protected override void Init()
    {
        player = GameObject.FindWithTag("Player").GetComponent<InputControllor>();
        base.Init();
    }
    //初始继承
    protected override void Update()
    {
        base.Update();
    }
    private void OnDisable()
    {
        m_blockImage.color = _NormalColor;
    }
    public override void OnPointerEnter()
    {
        if (Item != null)
        {
            bag.Info.SetActive(true);
            bag.Info.SetPosition(this.transform.position);
            bag.Info.SetText(Item.data);
        }
        base.OnPointerEnter();
    }
    public override void OnPointerExit()
    {
        bag.Info.SetActive(false);
        base.OnPointerExit();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            // Disable raycast blocking to avoid interfering with dragging
            //canvasGroup.blocksRaycasts = false;
            //            Debug.Log("BeginDrag");
            fadeIcon = Instantiate(fadeIconPrb, bag.transform);

            rectTransform = fadeIcon.GetComponent<RectTransform>();
            Vector2 localPointerPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(bag.transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition);
            rectTransform.anchoredPosition = localPointerPosition;

            Image image = fadeIcon.GetComponent<Image>();
            image.sprite = Item.data.icon;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the UI element with the mouse pointer
        if (rectTransform != null)
            rectTransform.anchoredPosition += eventData.delta; /*canvas.scaleFactor*/
        //        Debug.Log("OnDrag");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (rectTransform != null)
        {
            rectTransform = null;
        }
        if (fadeIcon != null)
        {
            DestroyImmediate(fadeIcon);
        }
        // Re-enable raycast blocking after dragging
        //canvasGroup.blocksRaycasts = true;
        //        Debug.Log("OnEndDrag");
        // Optionally, you can add logic here to snap the UI element to a grid or specific position

        #region 两个背包交换物品
        if (this.Item != null)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = eventData.position;

            GraphicRaycaster graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
            graphicRaycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                RectTransform targetRect = result.gameObject.GetComponent<RectTransform>();
                if (targetRect != null)
                {
                    //在这里处理放置的背包物体
                    BagBlock bagBlock = targetRect.gameObject.GetComponent<BagBlock>();
                    //Debug.Log($"OnEndDrag {targetRect.gameObject.name}");
                    bagBlock.Change(this);
                    return;
                }

            }
        }
        #endregion
    }
    public override void OnClick()
    {
        if (Input.GetMouseButtonUp(1))
        {
            //右键点击
            if (Item)
            {
                //打开物品操作栏
                BagItemMenu bagItemMenu = Instantiate(bagItemMenuPrb, transform);
                List<ItemMenuActionInfo> itemMenuActionInfos = new();
                itemMenuActionInfos.Add(new("丢弃", Pop));
                bagItemMenu.Init(itemMenuActionInfos);
            }
        }
        base.OnClick();
    }
    public void OnDrop(PointerEventData eventData)
    {
        //        Debug.Log($"OnDrop{gameObject.name}");
    }
    // public override void OnClick(RightClickBag rightClickBag)
    // {
    //     base.OnClick(rightClickBag);
    //     //上次点击没有选中物品
    //     if (rightClickBag.m_selectedBlock == null)
    //     {
    //         if (this.Empty())
    //         {
    //             rightClickBag.m_selectedBlock = null;
    //         }
    //         else
    //         {
    //             rightClickBag.m_selectedBlock = this;
    //         }
    //     }
    //     //上次点击选中了物品
    //     else
    //     {
    //         if (this.Empty())
    //         {
    //             //交换
    //             this.Change(rightClickBag.m_selectedBlock);
    //             rightClickBag.m_selectedBlock = null;
    //         }
    //         else
    //         {
    //             //交换
    //             this.Change(rightClickBag.m_selectedBlock);
    //             rightClickBag.m_selectedBlock = null;
    //         }
    //     }
    // }

    //添加到格子里
    public override void Push(Item item)
    {
        if (item == null)
        {
            this.Item = null;
        }
        else
        {
            item.transform.parent = transform;
            item.transform.localPosition = Vector3.zero;
            this.Item = item;
        }
    }
    public override void Pop()
    {
        if (Item == null)
        {
            return;
        }
        Item.transform.parent = null;
        // SpriteRenderer spriteRenderer = MyItem.GetComponent<SpriteRenderer>();
        // spriteRenderer.sortingLayerName = MyItem.LayerName;
        // spriteRenderer.sortingOrder = MyItem.LayerID;
        Item.transform.position = player.transform.position;
        Item.GetComponent<Collider2D>().enabled = true;
        Item.gameObject.transform.rotation = Quaternion.identity;
        Item.gameObject.transform.localScale = Vector3.one;
        Debug.Log($"Pop MyItem.gameObject.SetActive(true);");
        Item.gameObject.SetActive(true);
        //Icon为none
        //m_itemIcon.sprite = m_iconNone;
        this.Item = null;
    }
    public override void Change(BaseBlock bagBlock)
    {
        Item item = this.Item;
        this.Push(bagBlock.Get());
        bagBlock.Push(item);
        //FIXME 背包》空格子交换，潜在的风险
    }
    public override Item Get()
    {
        return Item;
    }
    public override bool Empty()
    {
        if (Get() == null)
            return true;
        else
            return false;
    }
}
