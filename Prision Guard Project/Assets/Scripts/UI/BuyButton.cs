using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace PrisionGuard
{
    public class BuyButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private BuyableSO _item;

        private void OnEnable()
        {
            _item.SubscribeBoughtEvent(UpdatePrice);
            _item.Currency.SubscribeEvent(UpdateUI);
            _button.onClick.AddListener(_item.Buy);
            UpdateUI();
        }

        private void OnDisable()
        {
            _item.UnsubscribeBoughtEvent(UpdatePrice);
            _item.Currency.UnsubscribeEvent(UpdateUI);
            _button.onClick.RemoveListener(_item.Buy);
        }

        private void UpdatePrice()
        {
            _priceText.text = _item.Price.ToString();
        }

        private void UpdateUI(int value = 0)
        {
            UpdatePrice();
            _button.interactable = _item.CanBuy();
        }
    }
}