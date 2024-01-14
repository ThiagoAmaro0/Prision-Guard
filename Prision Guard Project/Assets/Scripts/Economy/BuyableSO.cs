using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuyableSO", menuName = "Prision Guard Project/BuyableSO", order = 1)]
public class BuyableSO : ScriptableObject
{
    [SerializeField] private CurrencySO _currency;
    [SerializeField] protected int _price;

    public virtual int Price { get => _price; set => _price = value; }
    public CurrencySO Currency { get => _currency; set => _currency = value; }

    protected event Action _onBought;

    public bool CanBuy()
    {
        return Currency.GetValue() >= Price;
    }

    public void Buy()
    {
        if (CanBuy())
        {
            Currency.AddValue(-Price);
            _onBought?.Invoke();
        }
    }

    public void SubscribeBoughtEvent(Action function)
    {
        _onBought -= function;
        _onBought += function;
    }
    public void UnsubscribeBoughtEvent(Action function)
    {
        _onBought -= function;
    }
}
