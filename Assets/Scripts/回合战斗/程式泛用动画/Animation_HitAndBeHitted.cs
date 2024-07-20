using System;
using UnityEngine;

// float x1 = time_last;
// float x2 = time;
// float y1 = Curve_Hit.Evaluate(x1);
// float y2 = Curve_Hit.Evaluate(x2);
// float y_delta = y2 - y1;

// y += y_delta;
// transform.localPosition += new Vector3(0f, y_delta, 0f);

//通用的攻击和受击刚体动画, 提供占位符
public class Animation_HitAndBeHitted : Animation_Base
{
    public bool playing;

    public Which which;

    [Header("刚体动画曲线")]

    public AnimationCurve Curve_Hit;

    public AnimationCurve Curve_BeHitted;

    public enum Which
    {
        hit = 0,
        behitted
    }

    [Header("信息")]

    [Tooltip("稍晚的时间")] public float time_last;

    [Tooltip("稍早的时间")] public float time;

    [Tooltip("动画持续时间")] public float time_Anima;// 当前/上个播放的动画持续时间

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Play("hit");

        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Play("behitted");

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

    private void PerFrame()
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

    //--------------------------------------------------------------------------
    //公开方法
    //--------------------------------------------------------------------------

    public void Stop()
    {
        if (playing)
        {
            switch (which)
            {
                case Which.hit:
                    {
                        float y_delta = -Curve_Hit.Evaluate(time);
                        transform.localPosition += new Vector3(y_delta, 0f, 0f);
                        break;
                    }
                case Which.behitted:
                    {
                        float y_delta = -Curve_BeHitted.Evaluate(time);
                        transform.localPosition += new Vector3(y_delta, 0f, 0f);
                        break;
                    }
            }
        }

    }

    /// <summary>
    /// "hit" "behitted"
    /// </summary>
    /// <param name="Name_Anima">"hit" "behitted"</param>
    public void Play(string Name_Anima)
    {
        //如果在播放，则直接快进到播放完
        if (playing)
        {
            Stop();
        }

        if (string.Equals(Name_Anima, "hit", StringComparison.OrdinalIgnoreCase))
        {
            time_Anima = GetRightEndPoint(Curve_Hit).x;
            which = Which.hit;
        }
        else if (string.Equals(Name_Anima, "behitted", StringComparison.OrdinalIgnoreCase))
        {
            time_Anima = GetRightEndPoint(Curve_BeHitted).x;
            which = Which.behitted;
        }
        else
        {
            throw new NotImplementedException("未知动画的播放");
        }

        playing = true;
        time = 0f;
        time_last = 0f;
    }

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
