using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{

    [SerializeField] private float parralaxOffset = -0.15f;


    private Camera _camera;
    private Vector2 _startPos;
    private Vector2 _travel => (Vector2)_camera.transform.position - _startPos;


    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = _startPos + _travel * parralaxOffset;
    }
}
