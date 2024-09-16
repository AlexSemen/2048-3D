using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SavingLoading : MonoBehaviour
{
    [SerializeField] private FaceController _faceController;
    [SerializeField] private Player _player;
    [SerializeField] private LimitingMovements _limitingMovements;
    [SerializeField] private GameObject _panelLoadFace;
    [SerializeField] private StarGame _settingsStarGame;
    [SerializeField] private Audio _audio;
    [SerializeField] private StartMenuPanel _startMenuPanel;
    [SerializeField] private YandexSetGet _yandexSetGet;
    
    private const float _attemptLoadDelay = 0.25f;

    private float _currentLoadTime;
    private int _indexSave;
    private string _jsonString;
    private bool _isNeedSave = false;
    private bool _isNeedLoad = false;
    private GameData _saveData = new GameData();
    private GameData _loadData = new GameData();
    private Coroutine _loadCoroutine;
    private WaitForSecondsRealtime _waitForSecondsRealtime;

    private void OnEnable()
    {
        _player.ChangedCoins += CreateSaveRequest;
        _yandexSetGet.ToApplyYandex += Save;
    }

    private void OnDisable()
    {
        _player.ChangedCoins -= CreateSaveRequest;
        _yandexSetGet.ToApplyYandex -= Save;
    }

    private void Awake()
    {
        _waitForSecondsRealtime = new WaitForSecondsRealtime(_attemptLoadDelay);
        _indexSave = _yandexSetGet.GetIndexToApplyYandex();
    }

    public void Load(float loadTime)
    {
        if (_loadCoroutine != null)
            return;
        
        _loadCoroutine = StartCoroutine(StartAttemptsLoad(loadTime));
    }

    public void CreateSaveRequest()
    {
        if (_loadCoroutine != null || _isNeedLoad)
            return;

        _saveData.SetData(_player.GetPlayerData(), _faceController.GetFaceData(), 
            _limitingMovements.GetLimitingData(), _audio.IsTurnedOn);
        _isNeedSave = true;
    }

    public void LoadFaces()
    {
        _settingsStarGame.StartGame(_loadData.ShapeType, _loadData.IsLimitMove, _loadData.Points, _loadData.BlockValues);
        _startMenuPanel.UpdateViewButtons();
    }

    private void Save(int index)
    {
        if (_isNeedSave == false && index != _indexSave)
            return;

        _jsonString = JsonUtility.ToJson(_saveData);

#if !UNITY_EDITOR
        PlayerAccount.SetCloudSaveData(_jsonString);
#endif
    }

    private IEnumerator StartAttemptsLoad(float loadTime)
    {
        _isNeedSave = false;
        _currentLoadTime = loadTime;

        while (_currentLoadTime > 0)
        {
            if (TryLoad())
            {
                _currentLoadTime = 0;
            }
            
            _currentLoadTime -= _attemptLoadDelay;
            
            yield return _waitForSecondsRealtime;
        }

        _loadCoroutine = null;
    }

    private bool TryLoad()
    {
#if !UNITY_EDITOR
        PlayerAccount.GetCloudSaveData((data) => _jsonString = data);
#endif
        
        if (_jsonString == null || string.IsNullOrEmpty(_jsonString))
        {
            return false;
        }

        _loadData = JsonUtility.FromJson<GameData>(_jsonString);

        _player.СhangeСoins(_loadData.Coins);
        _audio.SetIsTurnedOn(_loadData.IsAudio);

        if (_loadData.IsFace)
        {
            if (_faceController.Faces == null || _faceController.Faces.Count == 0)
            {
                LoadFaces();
            }
            else
            {
                _panelLoadFace.SetActive(true);
            }
        }

        return true;
    }
}
