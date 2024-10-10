using Agava.YandexGames;
using UnityEngine;

namespace Yandex
{
    public class SDKMetric : MonoBehaviour
    {
        private void Start()
        {
#if !UNITY_EDITOR
        YandexGamesSdk.GameReady();
#endif
        }
    }
}
