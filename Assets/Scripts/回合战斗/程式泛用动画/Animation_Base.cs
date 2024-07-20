using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Base : MonoBehaviour
{
    [Header("信息")]

    public bool playing;

    [Tooltip("稍晚的时间")] public float time_last;

    [Tooltip("稍早的时间")] public float time;

    [Tooltip("动画持续时间")] public float time_Anima;// 当前/上个播放的动画持续时间

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

    //-------------------------------------------------------------------------------------
    //公开方法
    //-------------------------------------------------------------------------------------

    /// <summary>
    /// 开始占位符, unity提供的动画机默认0~1秒
    /// </summary>
    public void StartTip()
    {

    }

    /// <summary>
    /// 结束占位符, unity提供的动画机默认0~1秒
    /// </summary>
    public void EndTip()
    {

    }
}
