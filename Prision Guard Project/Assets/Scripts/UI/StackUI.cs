using TMPro;
using UnityEngine;
namespace PrisionGuard
{
    public class StackUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private StackManager _stack;

        private void Start()
        {
            _text.text = $"0 / {_stack.MaxCount}";
        }

        private void OnEnable()
        {
            _stack.SubscribeEvent(UpdateUI);
        }

        private void OnDisable()
        {
            _stack.UnsubscribeEvent(UpdateUI);
        }

        private void UpdateUI(int count)
        {
            _text.text = $"{count} / {_stack.MaxCount}";
        }
    }
}
