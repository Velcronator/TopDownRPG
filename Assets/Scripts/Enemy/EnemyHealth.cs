using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health = 3;
    [SerializeField] private AudioClip _hurtSFX;
    [SerializeField] private AudioClip _dieSFX;
    [SerializeField] private GameObject _DeathVFX;
    [SerializeField] private float _knockbackThrust = 15f;

    private AudioSource _audioSource;
    private Flash _flash;


    private int _currentHealth = 0;
    private Knockback _knockback;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentHealth = _health;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _knockback.GetKnockedback(PlayerController.Instance.transform, _knockbackThrust);
        Hurt();
    }

    private void Hurt()
    {
        _audioSource.Play();
        StartCoroutine(CheckForDeathRoutine());
    }

    private IEnumerator CheckForDeathRoutine()
    {
        yield return StartCoroutine(_flash.FlashRoutine());
        DetectDeath();
    }
    private IEnumerator PlayDeath()
    {
        yield return StartCoroutine(_flash.FlashRoutine());
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Instantiate(_DeathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
