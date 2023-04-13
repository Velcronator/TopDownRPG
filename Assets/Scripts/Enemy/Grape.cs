using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject _grapeProjectilePrefab;

    private Animator _grapeAnimator;
    private SpriteRenderer _spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        _grapeAnimator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Attack()
    {
        _grapeAnimator.SetTrigger(ATTACK_HASH);

        if (transform.position.x - PlayerController.Instance.transform.position.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    public void SpawnProjectileAnimationEvent()
    {
        Instantiate(_grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}
