using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CurrencySO", menuName = "Prision Guard Project/CurrencySO", order = 0)]
public class CurrencySO : ScriptableObject
{
    private int _value;
    private event Action<int> onValueChange;

    private void OnEnable()
    {
        _value = 0;
    }

    public void SubscribeEvent(Action<int> function)
    {
        onValueChange -= function;
        onValueChange += function;
    }
    public void UnsubscribeEvent(Action<int> function)
    {
        onValueChange -= function;
    }

    public void AddValue(int ammount)
    {
        _value += ammount;
        onValueChange?.Invoke(_value);
    }

    public int GetValue()
    {
        return _value;
    }

}