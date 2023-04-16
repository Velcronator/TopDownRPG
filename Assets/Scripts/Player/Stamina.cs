using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite _fullStaminaImage, _emptyStaminaImage;
    //[SerializeField] private int _timeBetweenStaminaRefresh = 3;
    [SerializeField] private float _timeBetweenStaminaRefresh = 3f;

    private Transform _staminaContainer;
    private int _startingStamina = 3;
    private int _maxStamina;
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

    protected override void Awake()
    {
        base.Awake();
        _maxStamina = _startingStamina;
        CurrentStamina = _startingStamina;
    }

    private void Start()
    {
        _staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }
    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaImages();
        StopAllCoroutines();
        StartCoroutine(RefreshStaminaRoutine());

    }

    public void RefreshStamina()
    {
        if(CurrentStamina < _maxStamina && !PlayerHealth.Instance.IsDead)
        {
            CurrentStamina++;
        }
        UpdateStaminaImages();
    }

    public void ReplenishStaminaOnDeath()
    {
        CurrentStamina = _startingStamina;
        UpdateStaminaImages();
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_timeBetweenStaminaRefresh);
            RefreshStamina();
        }
    }

    private void UpdateStaminaImages()
    {
        for (int i = 0; i < _maxStamina; i++)
        {
            Transform child = _staminaContainer.GetChild(i);
            Image image = child?.GetComponent<Image>();
            if(i <= CurrentStamina - 1)
            {
                image.sprite = _fullStaminaImage;
            }
            else
            {
                image.sprite = _emptyStaminaImage;
            }
        }
    }
}
