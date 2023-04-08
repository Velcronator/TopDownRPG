using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 22f;
    [SerializeField] private GameObject _particleOnHitPrefabVFX;
    [SerializeField] private bool _isEnemyProjectile = false;
    [SerializeField] private float _projectileRange = 10f;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float _projectileRange)
    {
        this._projectileRange = _projectileRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        if(!collision.isTrigger && (enemyHealth || indestructible || player))
        {
            if(player && _isEnemyProjectile)
            {
                player.TakeDamage(1, transform);
            }

            Instantiate(_particleOnHitPrefabVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(_startPosition, transform.position) > _projectileRange) { Destroy(gameObject); }
    }

    private void MoveProjectile()
    {
        this.transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
    }
}
