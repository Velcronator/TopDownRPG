using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    public static PlayerController Instance; //todo replace this

    [SerializeField] float _moveSpeed = 4f;
    [SerializeField] float _dashSpeed = 4f;
    [SerializeField] float _dashTime = 0.2f;
    [SerializeField] TrailRenderer _trailRenderer;
    [SerializeField] float _dashCoolDown = 0.25f;

    PlayerControls _playerControls;
    Vector2 _movement;
    Rigidbody2D _rb;
    Animator _animator;
    SpriteRenderer _spriteRenderer;

    private bool facingLeft = false;
    private bool isDashing = false;

    private void Awake()
    {
        Instance = this;
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerControls.Combat.Dash.performed += _ => Dash();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Disable();
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
            FacingLeft = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            _moveSpeed *= _dashSpeed;
            _trailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        yield return new WaitForSeconds(_dashTime);
        _moveSpeed /= _dashSpeed;
        _trailRenderer.emitting = false;
        yield return new WaitForSeconds(_dashCoolDown);
        isDashing = false;
    }
}
