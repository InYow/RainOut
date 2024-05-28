using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitEffect : MonoBehaviour
{
    private Animator _animator;
    private void OnEnable()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
        _animator.Play("hitEffect");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_animator == null)
            _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
