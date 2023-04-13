using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletMoveSpeed;
    [SerializeField] private int _burstCount;
    [SerializeField] private int _projectilesPerBurst;
    [SerializeField][Range(0, 359)] private float _angleSpread;
    [SerializeField] private float _startingDistance = 0.1f;
    [SerializeField] private float _timeBetweenBursts;
    [SerializeField] private float _restTime = 1f;
    [SerializeField] private bool _stagger;
    [SerializeField] private bool _oscillate;

    private bool _isShooting = false;

    private void OnValidate()
    {
        if (_oscillate) { _stagger = true; }
        if (!_oscillate) { _stagger = false; }
        if (_projectilesPerBurst < 1) { _projectilesPerBurst = 1; }
        if (_burstCount < 1) { _burstCount = 1; }
        if (_timeBetweenBursts < 0.1f) { _timeBetweenBursts = 0.1f; }
        if (_restTime < 0.1f) { _restTime = 0.1f; }
        if (_startingDistance < 0.1f) { _startingDistance = 0.1f; }
        if (_angleSpread == 0) { _projectilesPerBurst = 1; }
        if (_bulletMoveSpeed <= 0) { _bulletMoveSpeed = 0.1f; }
    }


    public void Attack()
    {
        if (!_isShooting)
        {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        _isShooting = true;
        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (_stagger) { timeBetweenProjectiles = _timeBetweenBursts / _projectilesPerBurst; }

        for (int i = 0; i < _burstCount; i++)
        {
            if (!_oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (_oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if (_oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            for (int j = 0; j < _projectilesPerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPosition(currentAngle);
                GameObject newBullet = Instantiate(_bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(_bulletMoveSpeed);
                }

                currentAngle += angleStep;

                if (_stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
            }

            currentAngle = startAngle;

            if (!_stagger) { yield return new WaitForSeconds(_timeBetweenBursts); }


        }

        yield return new WaitForSeconds(_restTime);
        _isShooting = false;

    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;
        if (_angleSpread != 0)
        {
            angleStep = _angleSpread / (_projectilesPerBurst - 1);
            halfAngleSpread = _angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPosition(float currentAngle)
    {
        float x = transform.position.x + _startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + _startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }

}
