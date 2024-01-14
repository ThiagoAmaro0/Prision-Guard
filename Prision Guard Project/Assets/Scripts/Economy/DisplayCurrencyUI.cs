using System;
using TMPro;
using UnityEngine;

public class DisplayCurrencyUI : MonoBehaviour
{
    [SerializeField] private CurrencySO _currency;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _currency.SubscribeEvent(UpdateUI);
        UpdateUI(_currency.GetValue());
    }

    private void OnDisable()
    {
        _currency.UnsubscribeEvent(UpdateUI);
    }

    private void UpdateUI(int value)
    {
        _text.text = value.ToString();
    }
}