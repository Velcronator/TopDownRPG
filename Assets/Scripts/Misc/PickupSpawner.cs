using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject _goldCoinPrefab;

    public void DropItems()
    {
        Instantiate(_goldCoinPrefab, transform.position, Quaternion.identity);
    }
}
