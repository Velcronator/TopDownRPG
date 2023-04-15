using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _knockbackThrust = 10f;
    [SerializeField] private float _damageRecoveryTime = 1f;

    private Slider _healthSlider;
    private int _currentHealth;
    private bool _canTakeDamage = true;

    private Knockback _knockback;
    private Flash _flash;
    const string HEALTH_SLIDER_TEXT = "Health Slider";

    protected override void Awake()
    {
        base.Awake();
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }
    private void Start()
    {
        _currentHealth = _maxHealth;
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();
        if (enemy)
        {
            TakeDamage(1, collision.transform);
        }
    }

    public void HealPlayer()
    {
        if(_currentHealth < _maxHealth) 
        {
            _currentHealth += 1;
            UpdateHealthSlider();
        }
    }

    public void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!_canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();
        _knockback.GetKnockedback(hitTransform, _knockbackThrust);
        StartCoroutine(_flash.FlashRoutine());
        _canTakeDamage = false;
        _currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckPlayerDeath();
    }

    private void CheckPlayerDeath()
    {
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Debug.Log("Player Death");
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (_healthSlider == null)
        {
            _healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();
        }

        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _currentHealth;
    }
}
