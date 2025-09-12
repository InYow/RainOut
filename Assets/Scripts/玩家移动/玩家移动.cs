using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class 玩家移动 : MonoBehaviour
{
    [Header("输入操作")]

    private MyInput myInput;

    public float speed;

    public bool walk;

    public bool Walk
    {
        get
        {
            return walk;
        }
        set
        {
            if (Walk != value)
            {
                if (value == true)
                {
                    _walk_audio_recover = 0.018f;
                }
                else
                {

                }
            }
            walk = value;
        }
    }

    public bool run;

    public float attack_recover;

    private float _attack_recover;

    public Vector2 move_dic;

    public Vector2 face_dic;

    private Rigidbody2D _rb;

    private Animator _animator;

    [Header("声音")]

    public List<AudioSource> audio_attack;

    public List<AudioSource> audio_walk;

    public float walk_audio_recover;

    public float _walk_audio_recover;

    public static 玩家移动 instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
            myInput = GameObject.FindObjectOfType<MyInput>();

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

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

        if (run && _walk_audio_recover > 0f)
        {
            _walk_audio_recover -= Time.deltaTime * 2;
        }
        else if (Walk && _walk_audio_recover > 0f)
        {
            _walk_audio_recover -= Time.deltaTime;
        }

    }

    void Update()
    {
        Timer();

        //操作输入
        if (_attack_recover <= 0f)
        {

            //行走方向
            //move_dic = Vector2.zero;

            move_dic = myInput.inputActions.回合战斗外.Move.ReadValue<Vector2>();

            // if (Input.GetKey(KeyCode.A))
            // {
            //     move_dic += new Vector2(-1f, 0f);
            // }
            // if (Input.GetKey(KeyCode.D))
            // {
            //     move_dic += new Vector2(1f, 0f);
            // }
            // if (Input.GetKey(KeyCode.W))
            // {
            //     move_dic += new Vector2(0f, 1f);
            // }
            // if (Input.GetKey(KeyCode.S))
            // {
            //     move_dic += new Vector2(0f, -1f);
            // }

            //面朝方向
            if (move_dic != Vector2.zero)
            {
                face_dic = move_dic;
            }

            //攻击
            if (myInput.inputActions.回合战斗外.Attack.IsPressed())
            {
                _animator.Play("attack_blend", 0, 0f);

                _attack_recover = attack_recover;

                move_dic = Vector2.zero;

                RandomPlay(audio_attack);
            }
        }

        //受击
        if (myInput.inputActions.回合战斗外.Hit.triggered)
        {
            _animator.Play("hit_blend", 0, 0f);

            move_dic = Vector2.zero;
        }

        if (move_dic != Vector2.zero)
        {
            Walk = true;
        }
        else
        {
            Walk = false;
        }

        //跑步
        if (Walk && myInput.inputActions.回合战斗外.Run.IsPressed())
            run = true;
        else
            run = false;


        //设置速度
        if (run)
            _rb.velocity = move_dic * speed * 2;
        else
            _rb.velocity = move_dic * speed;

        //行走声音
        if (Walk && _walk_audio_recover <= 0f)
        {
            WalkAudio();
            _walk_audio_recover = walk_audio_recover;
        }

        #region  动画机传递参数

        //行走
        if (Walk)
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
        if (myInput.inputActions.回合战斗外.Death.triggered)
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

    public void WalkAudio()
    {
        audio_walk[0].Play();
    }

    private List<Vector3> positions = new List<Vector3>();

    public List<Vector3> GetPositions()
    {
        return new List<Vector3>(positions);
    }
}
