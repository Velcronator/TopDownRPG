using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool _gettingKnockedBack {  get; private set; }

    Rigidbody2D rb;
    private float _knockbackTime = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedback(Transform damageSource, float knockbackThrust)
    {
        _gettingKnockedBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockbackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(_knockbackTime);
        rb.velocity = Vector2.zero;
        _gettingKnockedBack = false;
    }
}
