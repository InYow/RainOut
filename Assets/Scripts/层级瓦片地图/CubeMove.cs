using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CubeMove : MonoBehaviour
{
    public Color color = Color.red;
    [Header("物理信息")]
    public float speed;
    public Vector2 direction;
    [Header("碰撞层级")]
    public Height height;
    [Header("碰撞箱")]
    public float L;
    public float W;
    public Vector2 anchor;
    void Update()
    {
        //距离
        float distance = (speed * Time.deltaTime * direction.normalized).magnitude;
        //帧末位置
        Vector2 endposition = transform.position;
        int times = 0;
        //检测
        while (distance > 0f)
        {
            if (times > 1)
                Debug.Log(times);
            //预测下次碰撞
            RaycastHit2D[] hit2Ds = Physics2D.BoxCastAll(endposition + anchor, new(L, W), 0f, direction, distance);
            if (hit2Ds.Length > 0)
            {
                RaycastHit2D hit = new();
                float thisHeight = height.height;
                foreach (var hit2D in hit2Ds)
                {
                    float otherheight = hit2D.collider.GetComponent<Height>().height;
                    if (thisHeight <= otherheight)
                    {
                        hit = hit2D;
                        break;
                    }
                }
                //存在碰撞
                if (hit.collider != null)
                {
                    times++;
                    Vector2 indic = direction.normalized;
                    Vector2 outdic;
                    Vector2 normal = hit.normal;
                    outdic = (indic - 2f * Vector2.Dot(indic, normal) * normal).normalized;
                    //Debug.Log($"撞到{hit.collider.gameObject.name}, at{hit.point}, 入射方向{indic}, 法线{normal}, 出射方向{outdic}");
                    //改变方向
                    direction = outdic;
                    Vector2 theposition = hit.point + hit.normal * L;
                    //剩余距离
                    distance -= (theposition - endposition).magnitude;
                    //帧末位置
                    endposition = theposition;
                }
                else
                {
                    //不存在碰撞, 跳出循环
                    break;
                }
            }
            else
            {
                //不存在碰撞, 跳出循环
                break;
            }
        }
        //移动到帧末的位置
        endposition += direction * distance;
        transform.position = endposition;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position + (Vector3)anchor, new(L, W, 0f));
    }
}