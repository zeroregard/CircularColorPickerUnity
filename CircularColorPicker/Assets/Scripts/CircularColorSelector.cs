using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CircularColorSelector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private RectTransform _cursor;
    [SerializeField] private Image _colorPicked;
    private RectTransform _thisRect;
    private bool _pointerDown = false;
    private float _radius;
    void Awake()
    {
        _thisRect = transform as RectTransform;
        _radius = _thisRect.sizeDelta.x / 2;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pointerDown = false;
    }

    void Update()
    {
        if (_pointerDown == true)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, Input.mousePosition, null, out Vector2 localPoint);
            var mag = localPoint.magnitude;
            if (mag > _radius)
            {
                localPoint = new Vector2(localPoint.x / mag, localPoint.y / mag) * _radius;
            }
            _cursor.anchoredPosition = localPoint;
            SetColor(localPoint);
        }
    }

    void SetColor(Vector2 localPosition)
    {
        var normalized = new Vector2(localPosition.x / _radius, localPosition.y / _radius);
        var hue = Mathf.Atan2(normalized.y, normalized.x) / 2f;
        if (hue < 0)
        {
            hue += Mathf.PI;
        }
        hue = hue / Mathf.PI;
        var mag = normalized.magnitude;
        var rgb = Color.HSVToRGB(hue, mag, 1f);
        _colorPicked.color = rgb;
    }
}
