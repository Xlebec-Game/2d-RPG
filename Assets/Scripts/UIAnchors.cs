using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIAnchors : MonoBehaviour
{
    [SerializeField] private RectTransform[] _rects;
    private CanvasScaler _canvas;

    [ContextMenu("Set Anchors")]
    private void SetAnchors()
    {
        _canvas = GetComponent<CanvasScaler>();
        _rects = GetComponentsInChildren<RectTransform>().Skip(1).ToArray();

        foreach (var rect in _rects)
        {
            if (rect.anchorMin == Vector2.one / 2 && rect.anchorMax == Vector2.one / 2)
            {
                float x = rect.anchoredPosition.x / _canvas.referenceResolution.x;
                float y = rect.anchoredPosition.y / _canvas.referenceResolution.y;

                rect.anchoredPosition = Vector2.zero;
                rect.anchorMin = new(x + 0.5f, y + 0.5f);
                rect.anchorMax = new(x + 0.5f, y + 0.5f);

                float hor = rect.sizeDelta.x / _canvas.referenceResolution.x / 2;
                float ver = rect.sizeDelta.y / _canvas.referenceResolution.y / 2;
                rect.anchorMin = new(rect.anchorMin.x - hor, rect.anchorMin.y - ver);
                rect.anchorMax = new(rect.anchorMax.x + hor, rect.anchorMax.y + ver);
                rect.sizeDelta = Vector2.zero;
            }
        }
    }
}
