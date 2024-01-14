using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class SafeAreaContainer : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private CanvasScaler canvasScaler;
    private float bottomUnits, topUnits;
    private void Start()
    {
        ApplyVerticalSafeArea();
    }
    [ContextMenu("Update Safe Area")]
    public void ApplyVerticalSafeArea()
    {
        float bottomPixels = Screen.safeArea.y;
        float topPixel = Screen.currentResolution.height - (Screen.safeArea.y + Screen.safeArea.height);

        float bottomRatio = bottomPixels / Screen.currentResolution.height;
        float topRatio = topPixel / Screen.currentResolution.height;

        Vector2 referenceResolution = canvasScaler.referenceResolution;
        bottomUnits = referenceResolution.y * bottomRatio;
        topUnits = referenceResolution.y * topRatio;
        _transform.offsetMin = new Vector2(_transform.offsetMin.x, bottomUnits + 90);
        _transform.offsetMax = new Vector2(_transform.offsetMax.x, -topUnits);
    }

}
