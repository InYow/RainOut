using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : Item
{
    public SelectTileBox selectTileBoxPrb;
    public SelectTileBox selectTileBox;
    private Animator m_animator;
    public GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    public override void Use()
    {
        m_animator.Play("use");
    }
    public void HoeDirt()
    {
        HoeFloor.Instance.SetHoeDirt(selectTileBox.vector3int);
    }
    public override void OnCarryOn(Hand hand)
    {
        base.OnCarryOn(hand);
        selectTileBox = Instantiate(selectTileBoxPrb);
    }
    public override void OnDisCarry(Hand hand)
    {
        base.OnDisCarry(hand);
        Destroy(selectTileBox.gameObject);
        selectTileBox = null;
    }
}
