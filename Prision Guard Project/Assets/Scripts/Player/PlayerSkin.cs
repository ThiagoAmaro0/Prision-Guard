using System;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    [SerializeField] private BuyableSO _skinItem;
    [SerializeField] private Renderer _renderer;
    private MaterialPropertyBlock propertyBlock;

    private void Start()
    {
        propertyBlock = new MaterialPropertyBlock();
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
        propertyBlock.SetColor("_BaseColor", UnityEngine.Random.ColorHSV());
        _renderer.SetPropertyBlock(propertyBlock);
    }
}