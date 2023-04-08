using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    private Animator _Animator;

    private void Awake()
    {
        _Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(!_Animator) { return; }
        AnimatorStateInfo animatorStateInfo = _Animator.GetCurrentAnimatorStateInfo(0);
        _Animator.Play(animatorStateInfo.fullPathHash, -1, Random.Range(0.0f, 1.0f));
    }
}
