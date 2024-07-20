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
public class Animation_HitBeHittedAndSoOn : Animation_Base
{
    [Tooltip("交互对象")] public Transform trans;

    public Which which;

    [Header("刚体动画曲线")]

    public AnimationCurve Curve_Hit;

    public AnimationCurve Curve_BeHitted;

    public enum Which
    {
        hit = 0,
        behitted
    }

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

    private void PerFrame()
    {
        Vector2 Dic = Vector2.right;
        //计算交互对象方向
        if (trans != null)
        {
            Dic = new(trans.position.x - transform.position.x, trans.position.y - transform.position.y);
            Dic = Dic.normalized;
        }

        switch (which)
        {
            case Which.hit:
                {
                    float y_delta = Curve_Hit.Evaluate(time) - Curve_Hit.Evaluate(time_last);
                    transform.localPosition += y_delta * (Vector3)Dic;
                    break;
                }
            case Which.behitted:
                {
                    float y_delta = Curve_BeHitted.Evaluate(time) - Curve_BeHitted.Evaluate(time_last);
                    transform.localPosition += y_delta * (Vector3)Dic;
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
        Vector2 Dic = Vector2.right;
        //计算交互对象方向
        if (trans != null)
        {
            Dic = new(trans.position.x - transform.position.x, trans.position.y - transform.position.y);
            Dic = Dic.normalized;
        }

        if (playing)
        {
            switch (which)
            {
                case Which.hit:
                    {
                        float y_delta = -Curve_Hit.Evaluate(time);
                        transform.localPosition += y_delta * (Vector3)Dic;
                        break;
                    }
                case Which.behitted:
                    {
                        float y_delta = -Curve_BeHitted.Evaluate(time);
                        transform.localPosition += y_delta * (Vector3)Dic;
                        break;
                    }
            }
        }

    }

    /// <summary>
    /// "hit" "behitted"
    /// </summary>
    /// <param name="Name_Anima">"hit" "behitted"</param>
    public void Play(string Name_Anima, Transform trans)
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

        this.trans = trans;
    }


}
