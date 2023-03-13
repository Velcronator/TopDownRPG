using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private AudioClip _hurtSFX;
    [SerializeField] private AudioClip _dieSFX;
    private AudioSource _audioSource;


    private int _currentHealth = 0;
    private Knockback knockback;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        knockback.GetKnockedback(PlayerController.Instance.transform, 15f);
        Hurt();
        DetectDeath();
    }

    private void Hurt()
    {
        _audioSource.Play();
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
