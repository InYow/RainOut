using System;
using System.Collections;
using System.Collections.Generic;
using SuperTiled2Unity.Editor.LibTessDotNet;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

// float x1 = time_last;
// float x2 = time;
// float y1 = Curve_Hit.Evaluate(x1);
// float y2 = Curve_Hit.Evaluate(x2);
// float y_delta = y2 - y1;

// y += y_delta;
// transform.localPosition += new Vector3(0f, y_delta, 0f);

//通用的攻击和受击刚体动画
public class Animation_HitAndBeHitted : MonoBehaviour
{
    public AnimationCurve Curve_Hit;
    public AnimationCurve Curve_BeHitted;

    public float time_last;
    public float time;

    public float time_Anima;

    public enum Which
    {
        hit = 0,
        behitted
    }

    public Which which;

    public bool playing;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Play_Hit();
        }

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

    public void Play_Hit()
    {
        time_Anima = GetRightEndPoint(Curve_Hit).x;

        which = Which.hit;


        playing = true;

        time = 0f;
        time_last = 0f;
    }

    public void PerFrame()
    {
        switch (which)
        {
            case Which.hit:
                {
                    float y_delta = Curve_Hit.Evaluate(time) - Curve_Hit.Evaluate(time_last);
                    transform.localPosition += new Vector3(y_delta, 0f, 0f);
                    break;
                }
            case Which.behitted:
                {
                    float y_delta = Curve_BeHitted.Evaluate(time) - Curve_BeHitted.Evaluate(time_last);
                    transform.localPosition += new Vector3(y_delta, 0f, 0f);
                    break;
                }
            default:
                {
                    throw new NotImplementedException("未知动画的播放");
                }
        }


    }

    //-------------------------------------------------------------------------------------
    //工具性质的方法
    //--------------------------------------------------------------------------------------

    private Vector2 GetRightEndPoint(AnimationCurve curve)
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
