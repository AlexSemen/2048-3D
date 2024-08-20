using UnityEngine;

public class LocalizationBlock : MonoBehaviour
{
    [SerializeField] private LocalizationChecker _localizationChecker;
    [SerializeField] private GameObject _ru;
    [SerializeField] private GameObject _en;
    [SerializeField] private GameObject _tr;

    private void Start()
    {
        _ru.SetActive(false);
        _en.SetActive(false);
        _tr.SetActive(false);
        SetLand(_localizationChecker.LocalizationType);
    }

    private void SetLand(LocalizationType localizationType)
    {
        switch (localizationType)
        {
            case LocalizationType.ru:
                _ru.SetActive(true);
                break;

            case LocalizationType.tr:
                _tr.SetActive(true);
                break;

            default:
                _en.SetActive(true);
                break;
        }
    }
}
