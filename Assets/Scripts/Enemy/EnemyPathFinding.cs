using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{

    [SerializeField] float _moveSpeed = 2f;

    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    private Knockback _knockback;
    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_knockback.GettingKnockedBack) { return; }
        _rb.MovePosition(_rb.position + _moveDirection * (_moveSpeed * Time.fixedDeltaTime));
    }

    private void FlipSprite()
    {
        if (_moveDirection.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDirection = targetPosition;
        FlipSprite();
    }

    public void StopMoving()
    {
        _moveDirection = Vector3.zero;
    }
}
