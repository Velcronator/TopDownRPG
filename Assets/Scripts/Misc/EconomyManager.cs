using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EconomyManager : Singleton<EconomyManager>
{
    private TMP_Text _goldText;
    private int _currentGold = 0;

    const string COIN_AMOUNT_TEXT = "Gold Amount Text";

    public void UpdateCurrentGold()
    {
        _currentGold++;
        if (_goldText == null)
        {
            _goldText = GameObject.Find(COIN_AMOUNT_TEXT).GetComponent<TMP_Text>();
        }

        _goldText.text = _currentGold.ToString("D3");
    }
}
