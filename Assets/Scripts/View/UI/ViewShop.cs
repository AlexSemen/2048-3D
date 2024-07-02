using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewShop : MonoBehaviour
{
    [SerializeField] private OrientationChecker _orientationChecker;
    [SerializeField] private RectTransform _container;
    

    private Vector2 _vertical = new Vector2(600, 1000);
    private Vector2 _horizon = new Vector2(900, 700);

    private void OnEnable()
    {
        UpdateSizeDelta(_orientationChecker.IsVertical);
        _orientationChecker.ChangedVertical += UpdateSizeDelta;
    }

    private void OnDisable()
    {
        _orientationChecker.ChangedVertical -= UpdateSizeDelta;
    }

    private void UpdateSizeDelta(bool isVertical)
    {
        if (isVertical)
        {
            _container.sizeDelta = _vertical;
        }
        else
        {
            _container.sizeDelta = _horizon;
        }
    }
}
