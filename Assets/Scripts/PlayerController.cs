using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;

    private PlayerControls playControls;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Awake()
    {
        playControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
         rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime) );
    }

    private void PlayerInput()
    {
        movement = playControls.Movement.Move.ReadValue<Vector2>();
    }
}
