using UnityEngine;
using Agava.WebUtility;
using System;

public class CheckingFocus : MonoBehaviour
{
    public event Action<bool> ChangeFocus;

#if !UNITY_EDITOR
    private void OnEnable()
    {
        Application.focusChanged += OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeWeb;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackgroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeWeb;
    }

    private void OnInBackgroundChangeApp(bool inApp)
    {
        ChangeFocus?.Invoke(inApp);
        PauseGame(!inApp); 
    }

    private void OnInBackgroundChangeWeb(bool isBackground) 
    {
        ChangeFocus?.Invoke(!isBackground);
        PauseGame(isBackground);
    }

    private void PauseGame(bool value) 
    {
        Time.timeScale = value ? 0 : 1; 
    }
#endif
}

