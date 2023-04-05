using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private Transform _arrowSpawnPoint;

    private Animator _animator;
    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (_animator != null)
        {
            _animator.SetTrigger(FIRE_HASH);
        }
        GameObject newArrow = Instantiate(_arrowPrefab, _arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projectile>().UpdateWeaponInfo(_weaponInfo);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return _weaponInfo;
    }
}
