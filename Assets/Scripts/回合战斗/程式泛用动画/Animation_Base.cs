using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Base : MonoBehaviour
{
    //-------------------------------------------------------------------------------------
    //工具性质的方法
    //--------------------------------------------------------------------------------------

    protected Vector2 GetRightEndPoint(AnimationCurve curve)
    {
        // 确保曲线有关键帧
        if (curve.length > 0)
        {
            // 获取最后一个关键帧
            Keyframe lastKeyframe = curve[curve.length - 1];

            // 获取右端点的时间和值
            float rightEndTime = lastKeyframe.time;
            float rightEndValue = lastKeyframe.value;

            return new Vector2(rightEndTime, rightEndValue);
        }
        else
        {
            throw new NotImplementedException("曲线为空");
        }
    }

}
