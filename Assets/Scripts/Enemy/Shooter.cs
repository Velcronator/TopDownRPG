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

    private bool _isShooting = false;

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
        float startAngle, currentAngle, angleStep;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);

        for (int i = 0; i < _burstCount; i++)
        {
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
            }

            currentAngle = startAngle;

            yield return new WaitForSeconds(_timeBetweenBursts);
            TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep);


        }

        yield return new WaitForSeconds(_restTime);
        _isShooting = false;

    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;

        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        float endAngle = targetAngle;
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

        Vector2 pos = new Vector2(x,y);
        
        return pos;
    }

}
