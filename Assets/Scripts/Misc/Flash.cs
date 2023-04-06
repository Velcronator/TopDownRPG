using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _whiteFlashMaterial;
    [SerializeField] private Material _secondFlashMaterial;
    [SerializeField] private float _restoreDefaultMaterialTime = 0.05f;

    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        int numFlashes = 3;

        for (int i = 0; i < numFlashes; i++)
        {
            _spriteRenderer.material = _whiteFlashMaterial;
            yield return new WaitForSeconds(_restoreDefaultMaterialTime);
            _spriteRenderer.material = _secondFlashMaterial;
            yield return new WaitForSeconds(_restoreDefaultMaterialTime);
            _spriteRenderer.material = _defaultMaterial;
            yield return new WaitForSeconds(_restoreDefaultMaterialTime);
        }
    }
}
