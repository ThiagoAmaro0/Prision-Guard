using UnityEngine;
namespace PrisionGuard
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private BuyableSO _skinItem;
        [SerializeField] private Renderer _renderer;
        private MaterialPropertyBlock _propertyBlock;

        private void Start()
        {
            _propertyBlock = new MaterialPropertyBlock();
        }
        private void OnEnable()
        {
            _skinItem.SubscribeBoughtEvent(ChangeColor);
        }

        private void OnDisable()
        {
            _skinItem.UnsubscribeBoughtEvent(ChangeColor);
        }

        [ContextMenu("DEBUG Change Color")]
        private void ChangeColor()
        {
            _propertyBlock.SetColor("_BaseColor", UnityEngine.Random.ColorHSV());
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}