using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Breath : Animation_Base
{
    [Header("刚体动画曲线")]

    public AnimationCurve Curve_Breath;

    private void Update()
    {
        if (playing)
        {
            //更新时间信息
            time_last = time;
            time += Time.deltaTime;
            time_Anima = GetRightEndPoint(Curve_Breath).x;

            //动画播放
            PerFrame();
        }
    }

    private void PerFrame()
    {
        float y_delta = Curve_Breath.Evaluate(time % time_Anima) - Curve_Breath.Evaluate(time_last % time_Anima);
        transform.localScale += new Vector3(0f, y_delta, 0f);
    }
}
