using UnityEngine;

public class Hand : MonoBehaviour
{
    public InputControllor inputControllor;
    private Vector2 m_pointDic;
    public BagBlock carryBagBlock;//手上的格子
    [Tooltip("应当只读")] public Item CarryItem;
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
            carryBagBlock?.item?.Use();
        }
        #region 调试
        CarryItem = carryBagBlock.item;
        #endregion 
    }
    public void CarryOn(BagBlock bagBlock)
    {
        //Debug.Log("carryon");
        Item item = bagBlock.item;
        if (bagBlock != null)
        {
            if (item != null)
            {
                //Debug.Log("item!=null");
                #region item 一般性的代码
                #region 设置位置、层级、关闭碰撞箱
                Collider2D collider = item.GetComponent<Collider2D>();
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
                Debug.Log($"CarryOn item.gameObject.SetActive(true)");
                item.gameObject.SetActive(true);
                //标记已拾取 关闭拾取了的Trigger
                collider.enabled = false;
                #endregion
                #endregion
                item.OnCarryOn(this);
            }
            else
            {
                if (carryBagBlock != null)
                {

                    if (carryBagBlock.item != null)
                    {
                        Debug.Log("carryBagBlock.item != null");
                        #region 清空手持的物体
                        carryBagBlock.item.transform.parent = carryBagBlock.transform;
                        carryBagBlock.item.transform.localPosition = Vector3.zero;
                        carryBagBlock.item.gameObject.SetActive(false);
                        #endregion
                    }
                    else
                    {
                        //上一次操作是清空
                    }
                }
                else
                {

                }
            }
            carryBagBlock = bagBlock;
        }
        else
        {

        }
    }
}
