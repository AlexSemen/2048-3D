using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class Localization : MonoBehaviour
{
    [SerializeField] private LocalizationChecker _localizationChecker;
    [SerializeField] private string _ru;
    [SerializeField] private string _en;
    [SerializeField] private string _tr;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        SetLand(_localizationChecker.LocalizationType);
    }

    private void SetLand(LocalizationType localizationType)
    {
        switch (localizationType)
        {
            case LocalizationType.ru:
                _text.text = _ru;
                break;

            case LocalizationType.tr:
                _text.text = _tr;
                break;

            default:
                _text.text = _en;
                break;
        }
    }
}
