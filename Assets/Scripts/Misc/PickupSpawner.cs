using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject _goldCoin;
    [SerializeField] GameObject _healthGlobePrefab;
    [SerializeField] GameObject _staminaGlobePrefab;


    public void DropItems()
    {
        int randomNum = Random.Range(1, 5);

        switch (randomNum)
        {
            case 1:
                Instantiate(_staminaGlobePrefab, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(_healthGlobePrefab, transform.position, Quaternion.identity);
                break;
            case 3:
                int randomAmountOfGold = Random.Range(1, 4);
                for (int i = 0; i < randomAmountOfGold; i++)
                {
                    Instantiate(_goldCoin, transform.position, Quaternion.identity);
                }
                break;
        }

    }
}
