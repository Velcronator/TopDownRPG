using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _knockbackThrust = 10f;
    [SerializeField] private float _damageRecoveryTime = 1f;
    
    private int _currentHealth;
    private bool _canTakeDamage = true;

    private Knockback _knockback;
    private Flash _flash;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy && _canTakeDamage)
        {
            TakeDamage(1);
            _knockback.GetKnockedback(collision.gameObject.transform, _knockbackThrust);
            StartCoroutine(_flash.FlashRoutine());
        }
    }

    private void TakeDamage(int damageAmount)
    {
        _canTakeDamage = false;
        _currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }

}
