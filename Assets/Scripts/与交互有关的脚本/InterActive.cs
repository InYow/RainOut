using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

//模拟交互范围和交互执行体
public class InterActive : MonoBehaviour
{
    public Vector2 size;
    public Vector2 offset;
    public Bag bag;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        CheckAct();
        if (Input.GetKeyDown(KeyCode.F))
        {
            InterAct();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            bag.OpenOrClose();
        }
    }
    /// <summary>
    /// 碰撞检测
    /// </summary>
    public void CheckAct()
    {
        Collider2D[] collider2D_List = Physics2D.OverlapBoxAll((Vector2)transform.position + offset, (Vector2)size, 0);
        foreach (var collider in collider2D_List)//获取Collider
        {
            InterActable interActableObj = collider.GetComponent<InterActable>();
            if (interActableObj != null)
            {
                interActableObj.Check();
            }
        }
    }
    /// <summary>
    /// 交互
    /// </summary>
    public void InterAct()
    {
        Collider2D[] collider2D_List = Physics2D.OverlapBoxAll((Vector2)transform.position + offset, (Vector2)size, 0);
        foreach (var collider in collider2D_List)//获取Collider
        {
            InterActable interActable = collider.GetComponent<InterActable>();
            if (interActable != null)
            {
                InterActable.Intertype type = interActable.Active();
                if (type == InterActable.Intertype.Nullable)
                    return;
                //FIN Weapon和Item合并，都是放到背包里。
                //FIN 注释掉Pickable_Weapon
                // #region 可拾取武器到手上 Pickable_Weapon
                // else if (type == InterActable.Intertype.Pickable_Weapon)
                // {
                //     collider.transform.SetParent(transform);
                //     collider.transform.localPosition = Vector3.zero;
                //     Vector3 scale = collider.transform.lossyScale;
                //     scale.x *= Mathf.Sign(scale.x);
                //     collider.transform.localScale = scale;
                //     //标记已拾取 关闭拾取了的Trigger
                //     collider.enabled = false;
                //     //FIXME:修复持有者为生物类
                //     collider.GetComponent<Weapon>().master = transform.parent.GetComponent<InputControllor>();
                //     //登记拾取的枪
                //     handGun = collider.GetComponent<Weapon>();
                //     //关闭拾取枪的文本悬浮
                //     Item item = handGun.GetComponent<Item>();
                //     if (item == null)
                //         Debug.Log($"{gameObject.name}没有Item组件");
                //     else
                //         item.SeeText(false);
                //     return;
                // }
                // #endregion
                #region 可拾取物品进背包 Pickable_Item
                else if (type == InterActable.Intertype.Pickable_Item)
                {
                    Debug.Log($"InterAct SetActive(false)");
                    collider.gameObject.SetActive(false);
                    bag.Add(collider.gameObject);
                    //FIN:写完背包，写"进背包"
                    return;
                }
                #endregion
                #region 可以对话的对话框 Contact_Talk
                else if (type == InterActable.Intertype.Contact_Talk)
                {
                    return;
                }
                #endregion
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (Vector3)offset, (Vector3)size);
    }
}
