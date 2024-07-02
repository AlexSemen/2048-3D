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
    [SerializeField] private CheckAuthorization _ñheckAuthorization;
#if !UNITY_EDITOR
    private const string AnonymousName = "Anonymous";
#endif
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.ChangedCoins += SetCoinsText;
        _player.ChangedPoints += SetPointsText;
        _ñheckAuthorization.ChangedAuthorized += UpdateActiveLogInButton;
        _ñheckAuthorization.ChangedHasPersonalProfileDataPermission += UpdateRequestPersonalDataButton;
        _ñheckAuthorization.ChangedHasPersonalProfileDataPermission += UpdateNameText;
    }

    private void OnDisable()
    {
        _player.ChangedCoins -= SetCoinsText;
        _player.ChangedPoints -= SetPointsText;
        _ñheckAuthorization.ChangedHasPersonalProfileDataPermission -= UpdateActiveLogInButton;
        _ñheckAuthorization.ChangedHasPersonalProfileDataPermission -= UpdateRequestPersonalDataButton;
        _ñheckAuthorization.ChangedHasPersonalProfileDataPermission -= UpdateNameText;
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
            _name.text = AnonymousName;
        }
#endif
    }
}
