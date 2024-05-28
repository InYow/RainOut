using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int _damage;
    float _speed;
    Vector2 _dic;
    Rigidbody2D _rb;
    public GameObject HitEffect;//击中特效
    [Header("子弹的主人")]
    public InputControllor master;
    void Start()
    {
        Init();
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rb.velocity = _dic * _speed;
    }
    void Init()
    {
        if (master == null)
        {
            _damage = 1;
            _speed = 5.0f;
        }
    }
    public void Instantiate(Vector2 dic, int dama, float spee, InputControllor theMaster)
    {
        transform.rotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, dic));
        _dic = dic;
        _damage = dama;
        _speed = spee;
        master = theMaster;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            Boar boar = other.GetComponent<Boar>();
            if (boar != null)
            {
                Debug.Log(_damage);
                boar.ChangeHealth(-_damage);
            }
            //FIN: 设置hit特效可见，解transform，设置位置
            HitEffect.transform.parent = null;
            HitEffect.SetActive(true);
            HitEffect.transform.position = transform.position;
            //FIN: 设置hit特效不可见——写在hit特效动画中


            gameObject.SetActive(false);
        }
    }
}
