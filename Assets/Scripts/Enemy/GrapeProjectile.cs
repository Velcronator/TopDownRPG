using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float _grapeProjectileDuration;
    [SerializeField] private AnimationCurve _grapeAnimationCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject _grapeShadowPrefab;

    private void Start()
    {
        GameObject grapeShadow = Instantiate(_grapeShadowPrefab, transform.position + new Vector3(0f, -0.3f, 0f), Quaternion.identity);
        Vector3 playerPosition = PlayerController.Instance.transform.position;
        Vector3 grapeShadowStartPosition = grapeShadow.transform.position;
        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPosition));
        StartCoroutine(MoveGrapeShadowRooutine(grapeShadow, grapeShadowStartPosition, playerPosition));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;
        while (timePassed < _grapeProjectileDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _grapeProjectileDuration;
            float heightT = _grapeAnimationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }

        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRooutine(GameObject grapeShadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < _grapeProjectileDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _grapeProjectileDuration;
            grapeShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);
            yield return null;
        }
        Destroy(grapeShadow);
    }
}
