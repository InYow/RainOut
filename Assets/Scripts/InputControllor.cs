using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public class InputControllor : MonoBehaviour
{
    public float speed;
    public float rollspeed;
    public float rolltime;
    private float _rolltime;
    private float _horizontal;
    private float _vertical;
    private Vector2 _lookForward;
    Rigidbody2D _rb;
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    [Header("手的位置/拿枪的位置")]
    public Transform handTrans;
    Vector2 _pointDic;
    public Vector2 PointDic { get { return _pointDic; } }
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        Init();
    }
    void Init()
    {
        _rolltime = -1.0f;
        _lookForward.Set(1.0f, 0.0f);
    }
    // Update is called once per frame
    void Update()
    {
        #region 对话
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            DialogBox.Instance.Next();
        }
        #endregion

        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        //计算面朝方向/翻滚方向;
        if (_rolltime < 0.0f)
        {
            Vector2 forward = new Vector2(_horizontal, _vertical);
            if (!Mathf.Approximately(0.0f, forward.magnitude))
            {
                _lookForward = forward.normalized;
            }
        }
        //FINISH:计算鼠标指向，根据指向调整scale朝向
        Vector2 scrMousePos = Input.mousePosition;
        Vector2 scrHandPos = Camera.main.WorldToScreenPoint(handTrans.position);
        _pointDic = (scrMousePos - scrHandPos).normalized;
        Vector3 scale = transform.localScale;
        if (_pointDic.x < 0.0f)
            scale.x = -1;
        else if (_pointDic.x > 0.0f)
            scale.x = 1;
        transform.localScale = scale;
        //翻滚计时
        if (_rolltime > 0.0f)
            _rolltime -= Time.deltaTime;
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _rolltime = rolltime;
                //传递参数。Animator
                _animator.SetTrigger("roll");
            }
        }
        //传递参数。Animator
        _animator.SetFloat("speed", _rb.velocity.magnitude);
    }

    private void FixedUpdate()
    {
        //移动
        if (_rolltime < 0.0f)
        {
            Vector2 moveForward = new Vector2(_horizontal, _vertical);
            if (moveForward.magnitude > 1.0f)
            {
                moveForward = moveForward.normalized;
            }
            _rb.velocity = moveForward * speed;
        }
        //翻滚,向面朝方向翻滚
        else
        {
            _rb.velocity = _lookForward * rollspeed;
        }
    }
}
