using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class 玩家移动 : MonoBehaviour
{
    [Header("输入操作")]
    public float speed;

    public bool walk;

    public bool run;

    public float attack_recover;

    private float _attack_recover;

    public Vector2 move_dic;

    public Vector2 face_dic;

    private Rigidbody2D _rb;

    private Animator _animator;

    [Header("声音")]

    public List<AudioSource> audio_attack;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _animator = GetComponent<Animator>();
    }

    private void Timer()
    {
        if (_attack_recover > 0f)
        {
            _attack_recover -= Time.deltaTime;
        }
    }

    void Update()
    {
        Timer();

        //操作输入
        if (_attack_recover <= 0f)
        {

            //行走方向
            move_dic = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                move_dic += new Vector2(-1f, 0f);
            }
            if (Input.GetKey(KeyCode.D))
            {
                move_dic += new Vector2(1f, 0f);
            }
            if (Input.GetKey(KeyCode.W))
            {
                move_dic += new Vector2(0f, 1f);
            }
            if (Input.GetKey(KeyCode.S))
            {
                move_dic += new Vector2(0f, -1f);
            }

            //面朝方向
            if (move_dic != Vector2.zero)
            {
                face_dic = move_dic;
            }

            //攻击
            if (Input.GetKeyDown(KeyCode.J))
            {
                _animator.Play("attack_blend", 0, 0f);

                _attack_recover = attack_recover;

                move_dic = Vector2.zero;

                RandomPlay(audio_attack);
            }
        }

        //受击
        if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.H))
        {
            _animator.Play("hit_blend", 0, 0f);

            move_dic = Vector2.zero;
        }

        if (move_dic != Vector2.zero)
        {
            walk = true;
        }
        else
        {
            walk = false;
        }

        //跑步
        if (walk && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.K)))
            run = true;
        else
            run = false;

        //设置速度
        if (run)
            _rb.velocity = move_dic * speed * 2;
        else
            _rb.velocity = move_dic * speed;


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

        //死亡
        if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.D))
        {
            _animator.Play("death");
        }

        #endregion

    }

    void RandomPlay(List<AudioSource> audioSources)
    {
        int index = Random.Range(0, audioSources.Count);

        audioSources[index].Play();
    }
}
