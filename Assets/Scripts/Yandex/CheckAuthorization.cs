using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;

public class CheckAuthorization : MonoBehaviour
{
    [SerializeField] private SavingLoading _savingLoading;

    public event Action<bool> ChangedAuthorized;
    public event Action<bool> ChangedHasPersonalProfileDataPermission;

    private bool _isHasPersonalProfileDataPermission = false;
    private bool _isAuthorized = false;
#if !UNITY_EDITOR
    private IEnumerator Start()
    {
        if (PlayerAccount.IsAuthorized == false)
        {
            PlayerAccount.StartAuthorizationPolling(1500);
            ChangedAuthorized.Invoke(false);
            ChangedHasPersonalProfileDataPermission.Invoke(false);
        }

        bool _isCheckAuthorized = true;

        while (_isCheckAuthorized)
        {
            if (PlayerAccount.IsAuthorized)
            {
                if (_isAuthorized == false)
                {
                    SetAuthorized(true);
                }

                if (PlayerAccount.HasPersonalProfileDataPermission)
                {
                    if (_isHasPersonalProfileDataPermission == false)
                    {
                        SetHasPersonalProfileDataPermission(true);
                    }
                }
                else
                {
                    if (_isHasPersonalProfileDataPermission)
                    {
                        SetHasPersonalProfileDataPermission(false);
                    }
                }
            }
            else
            {
                if (_isAuthorized)
                {
                    SetAuthorized(false);
                    SetHasPersonalProfileDataPermission(false);
                }
            }

            yield return new WaitForSecondsRealtime(0.25f);
        }
    }

#endif
    public void OnAuthorizeButtonClick()
    {
        PlayerAccount.Authorize();
    }

    public void OnRequestPersonalDataClick()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();
    }

    private void SetAuthorized(bool isAuthorized)
    {
        _isAuthorized = isAuthorized;

        if (isAuthorized)
        {
            _savingLoading.CreateLoadRequest();
        }
        
        ChangedAuthorized.Invoke(isAuthorized);
    }

    private void SetHasPersonalProfileDataPermission(bool isHasPersonalProfileDataPermission)
    {
        _isHasPersonalProfileDataPermission = isHasPersonalProfileDataPermission;
        ChangedHasPersonalProfileDataPermission.Invoke(isHasPersonalProfileDataPermission);
    }
}
