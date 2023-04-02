using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 22f;

    private void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        this.transform.Translate(Vector3.right * _moveSpeed * Time.deltaTime);
    }
}
