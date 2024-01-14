using UnityEngine;
namespace PrisionGuard
{
    public class DeliveryPoint : MonoBehaviour
    {
        [SerializeField] private CurrencySO _currency;
        [SerializeField] private int _valuePerDelivery;
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent(out StackManager stack))
            {
                int count = stack.Delivery();
                _currency.AddValue(_valuePerDelivery * count);
            }
        }
    }
}
