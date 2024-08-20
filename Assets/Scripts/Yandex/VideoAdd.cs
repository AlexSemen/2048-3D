using UnityEngine;
using Agava.YandexGames;
using System;

public class VideoAdd : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private int _addCoins = 10;

    public event Action<bool> ChangeAudio;

    public void Show()
    {
#if !UNITY_EDITOR
        ShowVideo();
#endif
    }

    public void ShowVideo() =>
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardCallback, OnCloseCallback);

    private void OnOpenCallback() 
    {
        Time.timeScale = 0;
        ChangeAudio?.Invoke(false);
    }

    private void OnRewardCallback()
    {
        _player.—hange—oins(_addCoins);
    }

    private void OnCloseCallback()
    {
        Time.timeScale = 1;
        ChangeAudio?.Invoke(true);
    }
}
