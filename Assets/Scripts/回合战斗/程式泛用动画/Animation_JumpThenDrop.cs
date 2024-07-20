using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_JumpThenDrop : Animation_Base
{
    [Header("刚体动画曲线")]

    public AnimationCurve Curve_JumpThenDrop;

    private void Update()
    {
        if (playing)
        {
            //更新时间信息
            if (time < 64f)
            {
                time_last = time;
                time += Time.deltaTime;
            }

            //时间是否到了
            if (time >= time_Anima)
            {
                //设置时间，停止
                time = time_Anima;
                playing = false;
            }

            //动画播放
            PerFrame();
        }

    }
    public void PerFrame()
    {
        Vector2 Dic = Vector2.up;

        float y_delta = Curve_JumpThenDrop.Evaluate(time) - Curve_JumpThenDrop.Evaluate(time_last);
        transform.localPosition += y_delta * (Vector3)Dic;

    }

    //-------------------------------------------------------------------------------------
    //公开方法
    //-------------------------------------------------------------------------------------

    /// <summary>
    /// "hit" "behitted"
    /// </summary>
    /// <param name="Name_Anima">"hit" "behitted"</param>
    public void Play(Object ob)
    {
        //如果在播放，则直接快进到播放完
        if (playing)
        {
            Stop();
        }

        time_Anima = GetRightEndPoint(Curve_JumpThenDrop).x;

        playing = true;
        time = 0f;
        time_last = 0f;
    }

    public void Stop()
    {
        if (playing)
        {
            float y_delta = -Curve_JumpThenDrop.Evaluate(time);
            transform.localPosition += new Vector3(y_delta, 0f, 0f);
        }
    }
}
