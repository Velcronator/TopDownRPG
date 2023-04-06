using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] float _laserGrowTime = 2f;

    private bool _isGrowing = true;
    private float _laserRange;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider2D;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Indestructible>() && !collision.isTrigger) { _isGrowing = false; }
    }

    public void UpdateLaserRange(float laserRange)
    {
        this._laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;
        while (_spriteRenderer.size.x < _laserRange && _isGrowing)
        {
            timePassed += Time.deltaTime;
            float linerT = timePassed / _laserGrowTime;

            //sprite
            _spriteRenderer.size = new Vector2(Mathf.Lerp(1f, _laserRange, linerT), 1f);
            //collider
            _capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, _laserRange, linerT), _capsuleCollider2D.size.y); ;
            _capsuleCollider2D.offset = new Vector2(Mathf.Lerp(1f, _laserRange, linerT)/2, _capsuleCollider2D.offset.y); ;


            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
}
