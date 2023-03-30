using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        AnimatorStateInfo animatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
        m_Animator.Play(animatorStateInfo.fullPathHash, -1, Random.Range(0.0f, 1.0f));
    }
}
