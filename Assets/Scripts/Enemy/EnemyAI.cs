using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    private Rigidbody2D _rb;
    private State _state;
    private EnemyPathing _enemyPathfinding;

    private void Awake()
    {
        _enemyPathfinding = GetComponent<EnemyPathing>();
        _state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine("RoamingRoutine");
    }

    private IEnumerator RoamingRoutine()
    {
        while (_state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            _enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
