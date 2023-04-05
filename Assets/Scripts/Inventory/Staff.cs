using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private GameObject _magicLaser;
    [SerializeField] private Transform _magicLaserSpawnPoint;

    private Animator _animator;
    readonly int STAFF_HASH = Animator.StringToHash("Staff");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        Debug.Log("Staff Attack");
        //if (_animator != null)
        //{
        //    _animator.SetTrigger(STAFF_HASH);
        //}
        //GameObject newArrow = Instantiate(_magicLaser, _magicLaserSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        //newArrow.GetComponent<Projectile>().UpdateWeaponInfo(_weaponInfo);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return _weaponInfo;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
