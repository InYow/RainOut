using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Weapon : Item
{
    private SpriteRenderer spriteRenderer;
    public int damage;
    public float speed;
    public Bullet bulletPrefab;
    public float fireRate;
    public Transform GunMuzzle;
    public SpriteRenderer GunHuo;
    public GameObjPool bulletPool;//子弹缓存池
    public InputControllor master;
    public float _time;//射击冷却
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        LayerName = spriteRenderer.sortingLayerName;
        LayerID = spriteRenderer.sortingOrder;
        Init();
    }
    void Init()
    {
        _time = 1 / fireRate;
        //生成并设置缓冲池
        bulletPool = gameObject.AddComponent<GameObjPool>() as GameObjPool;
        bulletPool.SetPrefab(bulletPrefab.gameObject);
    }
    // Update is called once per frame?
    void Update()
    {
        if (master == null)
        {
            return;
        }
        //关闭枪口火焰
        if (GunHuo.enabled)
        {
            GunHuo.enabled = false;
        }
        //射击冷却
        if (_time > 0)
            _time -= Time.deltaTime;
        //FIN 射击改为沿枪身方向射击，手（旋转中心）、瞄具、枪口位于一条直线上。
        //FIN 外部调用开火方法-->点击左键，使用手上的物体Use();。
    }
    public override void Use()
    {
        Shot();
        base.Use();
    }
    void Shot()
    {
        //        Debug.Log("shot");
        if (_time > 0)
        {
            return;
        }
        //FIN:从池中获取子弹，设置一系列参数
        //设置位置，设置旋转
        GameObject bullet = bulletPool.Get();
        bullet.SetActive(true);
        bullet.transform.position = GunMuzzle.position;
        Vector2 dic = (Vector2)(GunMuzzle.position - transform.position);
        bullet.GetComponent<Bullet>().Instantiate(dic, damage, speed, master);
        _time = 1 / fireRate;
        //枪火
        GunHuo.enabled = true;
        //        Debug.Log(gameObject.transform.localRotation.eulerAngles);
    }
    public void Discard()
    {
        //解父参考系
        transform.SetParent(null);
        //还原旋转
        transform.localRotation = Quaternion.identity;
        //还原scale放缩
        transform.localScale = Vector3.one;
        master = null;
        //还原图层
        spriteRenderer.sortingLayerName = LayerName;
        spriteRenderer.sortingOrder = LayerID;
        GetComponent<Collider2D>().enabled = true;
    }
    public override void OnCarryOn(Hand hand)
    {
        base.OnCarryOn(hand);
        this.master = hand.inputControllor;
    }
}
