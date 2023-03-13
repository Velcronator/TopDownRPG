using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2f;
    
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Knockback _knockback;


    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_knockback._gettingKnockedBack) { return; }
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDirection = targetPosition;
    }
}
