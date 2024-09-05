using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class ViewPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _pointsText;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private Button _logInButton;
    [SerializeField] private Button _requestPersonalDataButton;
    [SerializeField] private CheckAuthorization _ÒheckAuthorization;
    [SerializeField] private LocalizationChecker _localizationChecker;
  
    private const string AnonymousRu = "¿ÌÓÌËÏ";
    private const string AnonymousEn = "Anonymous";
    private const string AnonymousTr = "Anonim";

    private string _anonymousName;
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        switch (_localizationChecker.LocalizationType)
        {
            case LocalizationType.ru:
                _anonymousName = AnonymousRu;
                break;

            case LocalizationType.tr:
                _anonymousName = AnonymousTr;
                break;

            default:
                _anonymousName = AnonymousEn;
                break;
        }
    }

    private void OnEnable()
    {
        _player.ChangedCoins += SetCoinsText;
        _player.ChangedPoints += SetPointsText;
        _ÒheckAuthorization.ChangedAuthorized += UpdateActiveLogInButton;
        _ÒheckAuthorization.ChangedHasPersonalProfileDataPermission += UpdateRequestPersonalDataButton;
        _ÒheckAuthorization.ChangedHasPersonalProfileDataPermission += UpdateNameText;
    }

    private void OnDisable()
    {
        _player.ChangedCoins -= SetCoinsText;
        _player.ChangedPoints -= SetPointsText;
        _ÒheckAuthorization.ChangedHasPersonalProfileDataPermission -= UpdateActiveLogInButton;
        _ÒheckAuthorization.ChangedHasPersonalProfileDataPermission -= UpdateRequestPersonalDataButton;
        _ÒheckAuthorization.ChangedHasPersonalProfileDataPermission -= UpdateNameText;
    }

    private void SetPointsText(int points)
    {
        _pointsText.text = points.ToString();
    }

    private void SetCoinsText()
    {
        _coinsText.text = _player.Coins.ToString();
    }
    private void UpdateActiveLogInButton(bool value)
    {
        _logInButton.gameObject.SetActive(!value);
    }

    private void UpdateRequestPersonalDataButton(bool value)
    {
        _requestPersonalDataButton.gameObject.SetActive(!value);
    }

    private void UpdateNameText(bool value)
    {
#if !UNITY_EDITOR
        if (value)
        {
            PlayerAccount.GetProfileData((result) =>
            {
                _name.text = result.publicName;
            });
        }
        else
        {
            _name.text = _anonymousName;
        }
#endif
    }
}
