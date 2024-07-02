using Agava.YandexGames;
using UnityEngine;

public class SDKMetric : MonoBehaviour
{
    private void Start()
    {
#if !UNITY_EDITOR
        YandexGamesSdk.GameReady();
#endif
    }
}
