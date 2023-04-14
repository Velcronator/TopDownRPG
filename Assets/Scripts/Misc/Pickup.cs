using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float _pickupDistance = 5f;
    [SerializeField] private float _accelerationRate = 0.5f;
    [SerializeField] private float _moveToPlayerSpeed = 3f;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _heightY = 1.5f;
    [SerializeField] private float _popupDuration = 1f;

    private Rigidbody2D _rb;

    private Vector3 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(AnimationCurveSpawnRoutine());
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

    private IEnumerator AnimationCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;

        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-2f, 2f);
        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < _popupDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _popupDuration;
            float heightT = _animationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, _heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);

            yield return null;
        }
    }
}
