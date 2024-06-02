using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PointMove : MonoBehaviour
{
    public float speed;
    public Vector2 direction;
    public Height height;
    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 获取鼠标当前的屏幕坐标
            Vector3 mousePosition = Input.mousePosition;

            // 将屏幕坐标转换为世界坐标
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 vector2 = worldPosition;
            Vector2 dictoset = (vector2 - (Vector2)transform.position).normalized;

            direction = dictoset;
        }

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
            RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(endposition, direction, distance);
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
                    //剩余距离
                    distance -= (hit.point - endposition).magnitude;
                    float derta = 0.0001f;
                    distance -= derta;
                    //帧末位置
                    endposition = hit.point + direction * derta;
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
}
