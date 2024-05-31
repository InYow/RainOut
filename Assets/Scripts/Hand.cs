using UnityEngine;

public class Hand : MonoBehaviour
{
    public InputControllor inputControllor;
    private Vector2 m_pointDic;
    public BagBlock carryBagBlock;//手上的格子
    public BagBlock CarryBagBlock
    {
        get
        {
            return carryBagBlock;
        }
        set
        {
            //之前的从手上离开
            if (CarryBagBlock != null)
            {
                Item itempast = CarryBagBlock.Item;
                if (itempast != null)
                {
                    itempast.OnDisCarry(this);
                }
            }
            //赋值
            carryBagBlock = value;
            //新的进到手上
            if (CarryBagBlock != null)
            {
                Item itemnow = CarryBagBlock.item1;
                if (itemnow != null)
                {
                    //Debug.Log("item!=null");
                    #region item 一般性的代码
                    Collider2D collider = itemnow.GetComponent<Collider2D>();
                    //层级
                    collider.transform.SetParent(transform);
                    //位置
                    collider.transform.localPosition = Vector3.zero;
                    //x-Scale
                    Vector3 scale = collider.transform.lossyScale;
                    scale.x *= Mathf.Sign(scale.x);
                    collider.transform.localScale = scale;
                    //z-rotate
                    collider.transform.localRotation = Quaternion.identity;
                    //Debug.Log(item.gameObject.transform.localRotation.eulerAngles);
                    //active
                    //Debug.Log($"CarryOn item.gameObject.SetActive(true)");
                    itemnow.gameObject.SetActive(true);
                    //标记已拾取 关闭拾取了的Trigger
                    collider.enabled = false;
                    #endregion
                    itemnow.OnCarryOn(this);
                    return;
                }
            }
            //新值进了个空的
        }
    }
    public Item carryItem;
    public Item CarryItem
    {
        get
        {
            return carryItem;
        }
        set
        {
            carryItem = value;
        }
    }
    void Update()
    {
        //FINISH:计算鼠标指向，根据指向调整scale朝向
        Vector2 scrMousePos = Input.mousePosition;
        Vector2 scrHandPos = Camera.main.WorldToScreenPoint(transform.position);
        m_pointDic = (scrMousePos - scrHandPos).normalized;
        //旋转手
        float angle = Vector2.SignedAngle(Vector2.right, m_pointDic);
        if (angle > 90)
            angle = 180 - angle;
        if (angle < -90)
            angle = -180 - angle;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
        if (Input.GetMouseButton(0))
        {
            //            Debug.Log("carryBagBlock?.item?.Use();");
            CarryBagBlock?.Item?.Use();
        }
        #region 调试
        CarryItem = CarryBagBlock.Item;
        #endregion 
    }
    public void CarryOn(BagBlock bagBlock)
    {
        //Debug.Log("carryon");

    }
}
