using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float _pickupDistance = 5f;
    [SerializeField] private float _accelerationRate = 0.5f;
    [SerializeField] private float _moveToPlayerSpeed = 3f;

    private Rigidbody2D _rb;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector3 playerPosition = PlayerController.Instance.transform.position;

        if(Vector3.Distance(transform.position, playerPosition) < _pickupDistance)
        {
            _moveDirection = (playerPosition - transform.position).normalized;
            _moveToPlayerSpeed += _accelerationRate;
        }
        else
        {
            _moveDirection = Vector3.zero;
            _moveToPlayerSpeed = 0f;
        }
    }

    private void FixedUpdate()
    {
        _rb.velocity = _moveDirection * _moveToPlayerSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            Destroy(gameObject);
        }
    }
}
