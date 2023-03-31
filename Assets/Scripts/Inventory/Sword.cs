using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _slashAnimPrefab;
    [SerializeField] private Transform _slashAnimSpawnPoint;
    [SerializeField] float _swordAttackCoolDown = 0.5f;

    private Animator _animator;
    private GameObject _slashAnim;
    private Transform _weaponCollider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _weaponCollider = PlayerController.Instance.GetWeaponCollider();
        _slashAnimSpawnPoint = GameObject.Find("Slash Animation Spawn Point").transform;
    }



    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
        _weaponCollider.gameObject.SetActive(true);
        _slashAnim = Instantiate(_slashAnimPrefab, _slashAnimSpawnPoint.position, Quaternion.identity);
        _slashAnim.transform.position = this.transform.position; StartCoroutine(AttackCDRoutine());
        StartCoroutine(AttackCDRoutine());
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(_swordAttackCoolDown);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }



    public void DoneAttackingAnimEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimationEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180f, 0f, 0f);
        if (PlayerController.Instance.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void SwingDownFlipAnimationEvent()
    {
        _slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0f, 0f);
        if (PlayerController.Instance.FacingLeft)
        {
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
