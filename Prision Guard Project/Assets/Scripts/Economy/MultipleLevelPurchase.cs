using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MultipleLevelPurchaseSO", menuName = "Prision Guard Project/MultipleLevelPurchaseSO", order = 2)]
public class MultipleLevelPurchase : BuyableSO
{
    [SerializeField] private int _pricePerLevel;
    private int _level;
    private event Action<int> _onLevelUp;
    public override int Price { get => _price + _pricePerLevel * _level; set => _price = value; }

    private void OnEnable()
    {
        _level = 0;
        _onBought -= LevelUp;
        _onBought += LevelUp;
    }

    [ContextMenu("Debug Level UP")]
    private void LevelUp()
    {
        _level++;
        _onLevelUp?.Invoke(_level);
    }
    public void SubscribeLevelUpEvent(Action<int> function)
    {
        _onLevelUp -= function;
        _onLevelUp += function;
    }
    public void UnsubscribeLevelUpEvent(Action<int> function)
    {
        _onLevelUp -= function;
    }
}