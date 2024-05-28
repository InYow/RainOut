using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class Boar : MonoBehaviour
{
    public int health;
    private int _health;
    public float speed;
    private Rigidbody2D _rb;
    private Animator _animator;
    private float _stateTime;
    public bool die;
    [Header("随机状态")]
    public Vector2 timeRange;
    float _timeMin;
    float _timeMax;
    float _time;
    public Vector2 rotateRange;
    float _rotateMin;
    float _rotateMax;
    Vector2 _direction;
    public enum State
    {
        move,
        idle
    }
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Init()
    {
        _health = health;
        if (timeRange.x > timeRange.y || timeRange.x <= 0.0f)
        {
            Debug.Log("timeRange输入值错误");
            return;
        }
        _timeMin = timeRange.x;
        _timeMax = timeRange.y;
        _time = (_timeMin + _timeMax) / 2;
        if (rotateRange.x > rotateRange.y || rotateRange.x < 0.0f)
        {
            Debug.Log("rotateRange输入值错误");
            return;
        }
        _rotateMin = rotateRange.x;
        _rotateMax = rotateRange.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (die)
        {
            return;
        }
        _time -= Time.deltaTime;
        if (_time < 0.0f)
        {
            //随机状态, 随机时间, 初始化状态
            RandomState();
            _time = Random.Range(_timeMin, _timeMax);
            StateInit();
        }
        //调整Renderer朝向
        Vector3 scale = transform.localScale;
        if (_rb.velocity.x < 0.0f)
            scale.x = -1;
        else if (_rb.velocity.x > 0.0f)
            scale.x = 1;
        transform.localScale = scale;
        //参数传递。animator
        _animator.SetFloat("speed", _rb.velocity.magnitude);
    }
    private void FixedUpdate()
    {
        StateActive();
        //执行状态操作
    }
    public void ChangeHealth(int value)
    {
        _health = Mathf.Clamp(_health + value, 0, health);
        Debug.Log($"{gameObject}的生命为{_health}");
        if (_health == 0 && !die)
        {
            Die();
        }
    }
    void Die()
    {
        die = true;
        _animator.SetBool("die", die);
        _time = -1.0f;
        _rb.velocity = Vector2.zero;

        //停止物理模拟
        _rb.simulated = false;
    }
    void RandomState()
    {
        state = (State)Random.Range(0, 2);
    }
    void StateInit()
    {
        switch (state)
        {
            case State.move:
                {
                    float rad = Mathf.Deg2Rad * Random.Range(_rotateMin, _rotateMax);
                    _direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                    break;
                }
            case State.idle:
                {
                    _rb.velocity = Vector2.zero;
                    break;
                }
            default:
                break;
        }
    }
    void StateActive()
    {
        switch (state)
        {
            case State.move:
                {
                    _rb.velocity = _direction * speed;
                    break;
                }
            case State.idle:
                {
                    break;
                }
            default:
                break;
        }
    }
}
