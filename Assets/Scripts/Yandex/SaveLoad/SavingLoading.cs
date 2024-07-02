using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class SavingLoading : MonoBehaviour
{
    [SerializeField] private FaceController _faceController;
    [SerializeField] private Player _player;
    [SerializeField] private LimitingMovements _limitingMovements;
    [SerializeField] private InAdd _inAdd;
    [SerializeField] private GameObject _panelLoadFace;
    [SerializeField] private StarGame _settingsStarGame;
    [SerializeField] private Audio _audio;
    [SerializeField] private StartMenuPanel _startMenuPanel;

    private const float _loadTime = 5;
    private const float _attemptLoadDelay = 0.25f;
    private const float _timeDelay = 3.1f;

    private float _currentLoadTime;
    private float _currentTimeDelay;
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
    }

    private void OnDisable()
    {
        _player.ChangedCoins -= CreateSaveRequest;
    }

    private void Awake()
    {
        _waitForSecondsRealtime = new WaitForSecondsRealtime(_attemptLoadDelay);
    }

    private void Start()
    {
        _loadCoroutine = StartCoroutine(StartAttemptsLoad());
    }

    private void Update()
    {
        if (_currentTimeDelay > 0)
            _currentTimeDelay -= Time.deltaTime;

        if (_currentTimeDelay > 0)
            return;

        if (_isNeedSave && _isNeedLoad == false)
        {
            Save();

            _isNeedSave = false;
            _currentTimeDelay = _timeDelay;
        }

        if (_isNeedLoad)
        {
            if (_loadCoroutine != null)
                return;

            _loadCoroutine = StartCoroutine(StartAttemptsLoad());

            _isNeedLoad = false;
            _currentTimeDelay = _timeDelay;
        }

    }

    public void CreateSaveRequest()
    {
        if (_loadCoroutine != null || _isNeedLoad)
            return;

        _saveData.SetData(_player.GetPlayerData(), _faceController.GetFaceData(), 
            _limitingMovements.GetLimitingData(), _inAdd.IsBuyNoSticky, _audio.IsTurnedOn);
        _isNeedSave = true;
    }

    public void CreateLoadRequest()
    {
        if (_loadCoroutine != null)
            return;

        _isNeedLoad = true;
        _isNeedSave = false;
    }

    private void Save()
    {
        _jsonString = JsonUtility.ToJson(_saveData);

#if !UNITY_EDITOR
        PlayerAccount.SetCloudSaveData(_jsonString);
#endif
    }

    private IEnumerator StartAttemptsLoad()
    {
        _isNeedLoad = false;
        _isNeedSave = false;
        _currentLoadTime = _loadTime;

        while (_currentLoadTime > 0)
        {
#if !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
            {
                if (TryLoad())
                {
                    _currentLoadTime = 0;
                }
            }
#endif
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

        _inAdd.SetIsBuyNoSticky(_loadData.IsNoSticky);
        _player.—hange—oins(_loadData.Coins);
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

    public void LoadFaces()
    {
        _settingsStarGame.StartGame(_loadData.ShapeType, _loadData.IsLimitMove, _loadData.Points, _loadData.BlockValues);
        _startMenuPanel.UpdateViewButtons();
    }
}
