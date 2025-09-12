using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class 被动行走播放动画 : MonoBehaviour
{
    public Vector2 lastPos;

    public Vector2 face_dic;

    public float speed;

    public bool walk;

    public bool run;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        lastPos = transform.position;
    }

    private void Update()
    {
        Vector2 move_Dic = (Vector2)transform.position - lastPos;

        if (move_Dic == Vector2.zero)
        {
            walk = false;
        }
        else
        {
            walk = true;
        }

        //面朝方向
        if (move_Dic != Vector2.zero)
        {
            face_dic = move_Dic.normalized;
        }

        if (walk && move_Dic.magnitude >= speed * 2 * Time.deltaTime)
        {
            run = true;
        }
        else
        {
            run = false;
        }

        #region  动画机传递参数

        //行走
        if (walk)
        {
            _animator.SetBool("walk", true);
        }
        else
        {
            _animator.SetBool("walk", false);
        }


        //跑步
        if (run)
        {
            _animator.SetBool("run", true);
        }
        else
        {
            _animator.SetBool("run", false);
        }

        //面朝方向
        _animator.SetFloat("face_dic_x", face_dic.x);
        _animator.SetFloat("face_dic_y", face_dic.y);

        #endregion

        lastPos = (Vector2)transform.position;
    }
}
