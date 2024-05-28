using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoe : MonoBehaviour
{
    private Animator m_animator;
    public GameObject head;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Use();
        }
    }
    public void Use()
    {
        m_animator.Play("use");
    }
    public void HoeDirt()
    {
        HoeFloor.Instance.SetHoeDirt(head.transform.position);
    }
}
