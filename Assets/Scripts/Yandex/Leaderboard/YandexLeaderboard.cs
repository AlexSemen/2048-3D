using Agava.YandexGames;
using System;
using System.Collections.Generic;
using UnityEngine;

public class YandexLeaderboard : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ViewLeaderboard _viewLeaderboard;
    [SerializeField] private ViewButtons _viewButtons;
    [SerializeField] private GameObject _loadPanel;
    [SerializeField] private CheckAuthorization _�heckAuthorization;

    private const string LeaderboardClassic = "Classic";
    private const string LeaderboardLine = "Line";
    private const string LeaderboardCub = "Cub";
    private const string LeaderboardLimit = "Limit";
    private const string AnonymousName = "Anonymous";
    private const float _timeBetween = 5.1f;

    private bool _isWork = false;
    private ShapeType _loadShapeType = ShapeType.Null;
    private ShapeType _saveShapeType = ShapeType.Null;
    private bool _isLoadLimit = false;
    private bool _isSaveLimit = false;
    private string _saveLeaderboard;
    private string _loadLeaderboard = "";
    private string _uniqueID;
    private string _rank;
    private string _score;
    private string _name;
    private LeaderboardPlayer _newLeaderboardPlayers;
    private int _newScore;
    private float _delay = -1;
    private bool _isNeedSet = false;

    private Dictionary<string, List<LeaderboardPlayer>> _leaderboardsDatas = new Dictionary<string, List<LeaderboardPlayer>>();
    private Dictionary<string, LeaderboardPlayer> _leaderboardsDatasPersonal = new Dictionary<string, LeaderboardPlayer>();
    private List<string> _nameLeaderboards = new List<string>();
    private int _indexLeaderboard = 0;

    private void Awake()
    {
        _viewButtons.Init();

        _nameLeaderboards.Add(LeaderboardClassic);
        _nameLeaderboards.Add(LeaderboardLine);
        _nameLeaderboards.Add(LeaderboardCub);
        _nameLeaderboards.Add(LeaderboardLine + LeaderboardLimit);
        _nameLeaderboards.Add(LeaderboardCub + LeaderboardLimit);

        foreach (string name in _nameLeaderboards)
        {
            _leaderboardsDatas.Add(name, new List<LeaderboardPlayer>());
            _leaderboardsDatasPersonal.Add(name, null);
        }
    }

    private void OnEnable()
    {
        _player.ChangedPoints += CreateSetRequest;
        _�heckAuthorization.ChangedAuthorized += SetWork;
    }

    private void OnDisable()
    {
        _player.ChangedPoints -= CreateSetRequest;
        _�heckAuthorization.ChangedAuthorized += SetWork;
    }

    private void Update()
    {
        if(_isWork == false) 
            return;

        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
        }

        if (_delay <= 0)
        {
            if (_indexLeaderboard < _nameLeaderboards.Count)
            {
                LoadLeaderboard(_nameLeaderboards[_indexLeaderboard++]);
            }
            else
            {
                if (_isNeedSet)
                {
                    SetPlayerScore();
                }

                _indexLeaderboard = 0;
            }

            _delay = _timeBetween;
        }
    }

    public void TurnOnPanel()
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        _viewLeaderboard.gameObject.SetActive(true);

        if (_saveShapeType == ShapeType.Null)
        {
            SetLoadLeaderboard(ShapeType.Classic, false);
        }
        else
        {
            SetLoadLeaderboard(_saveShapeType, _isSaveLimit);
        }
    }

    public void SetSaveLeaderboard(ShapeType shapeType, bool limit)
    {
        _saveShapeType = shapeType;
        _isSaveLimit = limit;
        _saveLeaderboard = GetLeaderboard(shapeType, limit);
    }

    public void OnClickClassic()
    {
        SetLoadLeaderboard(ShapeType.Classic, _isLoadLimit);
    }

    public void OnClickLine()
    {
        SetLoadLeaderboard(ShapeType.Line, _isLoadLimit);
    }

    public void OnClickCub()
    {
        SetLoadLeaderboard(ShapeType.Cub, _isLoadLimit);
    }

    public void OnClickLimit()
    {
        if (_loadShapeType != ShapeType.Classic)
        {
            SetLoadLeaderboard(_loadShapeType, !_isLoadLimit);
        }
        else
        {
            _isLoadLimit = !_isLoadLimit;
            _viewButtons.UpdateButtons(_loadShapeType, _isLoadLimit);
        }
    }

    private void SetLoadLeaderboard(ShapeType shapeType, bool isLimit)
    {
        if (_loadShapeType != shapeType || _isLoadLimit != isLimit)
        {
            _loadShapeType = shapeType;
            _isLoadLimit = isLimit;

            _loadLeaderboard = GetLeaderboard(shapeType, isLimit);

            Fill();
            _viewButtons.UpdateButtons(_loadShapeType, _isLoadLimit);
        }
    }

    private string GetLeaderboard(ShapeType shapeType, bool isLimit)
    {
        string leaderboard;

        switch (shapeType)
        {
            case ShapeType.Classic:
                leaderboard = LeaderboardClassic;
                break;

            case ShapeType.Line:
                leaderboard = LeaderboardLine;
                break;

            case ShapeType.Cub:
                leaderboard = LeaderboardCub;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        if (isLimit && shapeType != ShapeType.Classic)
        {
            leaderboard += LeaderboardLimit;
        }

        return leaderboard;
    }

    private void CreateSetRequest(int score)
    {
        _newScore = score;
        _isNeedSet = true;
    }

    private void SetPlayerScore()
    {
#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.GetPlayerEntry(_saveLeaderboard, (result) =>
        {
            if (result == null || result.score < _newScore)
                Leaderboard.SetScore(_saveLeaderboard, _newScore);
        });
        _isNeedSet = false;
#endif
    }

    private void LoadLeaderboard(string leaderboard)
    {
#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;

        PlayerAccount.GetProfileData((result) =>
        {
            _uniqueID = result.uniqueID;
        });

        _leaderboardsDatas[leaderboard].Clear();

        Debug.Log(leaderboard);

        Leaderboard.GetEntries(leaderboard, (result) =>
        {
            foreach (var entry in result.entries)
            {
                _rank = entry.rank.ToString();
                _score = entry.score.ToString();
                _name = entry.player.publicName;

                if (string.IsNullOrEmpty(_name))
                    _name = AnonymousName;

                _newLeaderboardPlayers = new LeaderboardPlayer(_rank, _name, _score);
               
                if (entry.player.uniqueID == _uniqueID)
                {
                    _leaderboardsDatasPersonal[leaderboard] = _newLeaderboardPlayers;
                    Debug.Log("�������� ");
                    Debug.Log(_leaderboardsDatasPersonal[leaderboard].Rank + " " + _leaderboardsDatasPersonal[leaderboard].Name + " " + _leaderboardsDatasPersonal[leaderboard].Score);
                    Debug.Log("�������� ");
                }

                _leaderboardsDatas[leaderboard].Add(_newLeaderboardPlayers);
            }

            if (_viewLeaderboard.gameObject.activeSelf && _loadLeaderboard.Equals(leaderboard))
            {
                Fill();
            }
        });
#endif
    }

    private void Fill()
    {
#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;

        Debug.Log("Count = " + _leaderboardsDatas[_loadLeaderboard].Count);

        if (_leaderboardsDatas[_loadLeaderboard].Count > 0)
        {
            foreach (LeaderboardPlayer player in _leaderboardsDatas[_loadLeaderboard])
            {
                Debug.Log(player.Rank + " " + player.Name + " " + player.Score + " !!!!!!");
            }
        }

        _loadPanel.gameObject.SetActive(_leaderboardsDatas[_loadLeaderboard].Count == 0);
        _viewLeaderboard.ConstructLeaderboard(_leaderboardsDatas[_loadLeaderboard], _leaderboardsDatasPersonal[_loadLeaderboard]);
#endif
    }

    private void SetWork(bool value) 
    {
        if (value)
        {
            _delay = _timeBetween;
            _isWork = true;
        }
        else
        {
            _isWork = false;
        }
    }
}

