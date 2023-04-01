using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } }

    [SerializeField] float _moveSpeed = 4f;
    [SerializeField] float _dashSpeed = 4f;
    [SerializeField] float _dashTime = 0.2f;
    [SerializeField] TrailRenderer _trailRenderer;
    [SerializeField] float _dashCoolDown = 0.25f;
    [SerializeField] GameObject _dashParticlePrefab;
    [SerializeField] Transform _dashParticleSpawnPoint;

    [SerializeField] private Transform _weaponCollider;
    [SerializeField] private Transform _slashAnimSpawnPoint;

    PlayerControls _playerControls;
    Vector2 _movement;
    Rigidbody2D _rb;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    private bool facingLeft = false;
    private bool isDashing = false;
    private float _startingMoveSpeed;

    protected override void Awake()
    {
        base.Awake();

        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerControls.Combat.Dash.performed += _ => Dash();
        _startingMoveSpeed = _moveSpeed;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    public Transform GetWeaponCollider()
    {
        return _weaponCollider;
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + _movement * (_moveSpeed * Time.fixedDeltaTime));
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _animator.SetFloat("moveX", _movement.x);
        _animator.SetFloat("moveY", _movement.y);
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenPoint.x)
        {
            _spriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            _moveSpeed *= _dashSpeed;
            _trailRenderer.emitting = true;
            Instantiate(_dashParticlePrefab , _dashParticleSpawnPoint.position, Quaternion.identity);
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(_dashTime);
        _moveSpeed = _startingMoveSpeed;
        _trailRenderer.emitting = false;
        yield return new WaitForSeconds(_dashCoolDown);
        isDashing = false;
    }

    internal Transform GetSlashAnimSpawnPoint()
    {
        return _slashAnimSpawnPoint;
    }
}
