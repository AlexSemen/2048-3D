using Agava.YandexGames;
using UnityEngine;

public class LocalizationChecker : MonoBehaviour
{
    [SerializeField] private LocalizationText _logInLanguage;
    [SerializeField] private LocalizationText _shopLanguage;
    
    private const string Russian = "ru";
    private const string Turkish = "tr";

    private LocalizationType _localizationType = LocalizationType.en;

    public LocalizationType LocalizationType => _localizationType;

    private void Awake()
    {
#if !UNITY_EDITOR
        ChangeLanguage();
#endif
    }

    private void ChangeLanguage()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        switch (languageCode)
        {
            case Russian:
                _localizationType = LocalizationType.ru;
                break;

            case Turkish:
                _localizationType = LocalizationType.tr;
                break;

            default:
                _localizationType = LocalizationType.en;
                break;
        }
    }
}
