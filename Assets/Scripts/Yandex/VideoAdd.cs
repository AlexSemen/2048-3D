using UnityEngine;
using Agava.YandexGames;

public class VideoAdd : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private int _addCoins = 5;

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
        _audioSource.volume = 0;
    }

    private void OnRewardCallback()
    {
        _player.—hange—oins(_addCoins);
    }

    private void OnCloseCallback()
    {
        Time.timeScale = 1;
        _audioSource.volume = 1;
    }
}
