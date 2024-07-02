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

    private const string AnonymousName = "Anonymous";
    private const float _timeBetween = 5.1f;
   // private const float _timeBetweenSets = 1.5f;

    private ShapeType _loadShapeType = ShapeType.Null;
    private ShapeType _saveShapeType = ShapeType.Null;
    private bool _isLoadLimit = false;
    private bool _isSaveLimit = false;
    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();
    private string _saveLeaderboard;
    private string _loadLeaderboard;
    private string _uniqueID;
    private string _rank;
    private string _score;
    private string _name;
    private LeaderboardPlayer _leaderboardPlayerPersonal;
    private LeaderboardPlayer _newLeaderboardPlayers;
    private int _newScore;
    //private float _delaySet = -1;
    private float _delay = -1;
    private bool _isNeedSet = false;
    private bool _isNeedGet = false;

    private void Awake()
    {
        _viewButtons.Init();
    }

    private void OnEnable()
    {
        _player.ChangedPoints += CreateSetRequest;
        _viewLeaderboard.Disable += ClearLoadLeaderboard;
    }

    private void OnDisable()
    {
        _player.ChangedPoints -= CreateSetRequest;
        _viewLeaderboard.Disable -= ClearLoadLeaderboard;
    }

    private void Update()
    {
        if (_delay > 0)
        {
            _delay -= Time.deltaTime;
        }

        if (_isNeedSet && _delay <= 0)
        {
            _delay = _timeBetween;
            
            SetPlayerScore();

            _isNeedSet = false;
        }

        if (_isNeedGet && _delay <= 0 && _isNeedSet == false)
        {
            _delay = _timeBetween;
            
            Fill();

            _isNeedGet = false;
            _loadPanel.SetActive(false);
        }
    }

    public void TurnOnPanel()
    {
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
        SetLeaderboard(ref _saveLeaderboard, shapeType, limit);
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
        }

        _viewButtons.UpdateButtons(_loadShapeType, _isLoadLimit);
    }

    private void SetLoadLeaderboard(ShapeType shapeType, bool isLimit)
    {
        if (_loadShapeType != shapeType || _isLoadLimit != isLimit) 
        {
            _loadShapeType = shapeType;
            _isLoadLimit = isLimit;

            if(shapeType != ShapeType.Classic)
            {
                SetLeaderboard(ref _loadLeaderboard, shapeType, isLimit);
            }
            else
            {
                SetLeaderboard(ref _loadLeaderboard, shapeType, false);
            }

            CreateGetRequest();
            _viewButtons.UpdateButtons(_loadShapeType, _isLoadLimit);
        }
    }

    private void SetLeaderboard(ref string leaderboard, ShapeType shapeType, bool isLimit)
    {
        switch (shapeType)
        {
            case ShapeType.Classic:
                leaderboard = LeaderboardType.Classic.ToString();
                break;

            case ShapeType.Line:
                leaderboard = LeaderboardType.Line.ToString();
                break;

            case ShapeType.Cub:
                leaderboard = LeaderboardType.Cub.ToString();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        if (isLimit && shapeType != ShapeType.Classic)
        {
            leaderboard += LeaderboardType.Limit.ToString();
        }
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
#endif
    }

    private void CreateGetRequest()
    {
        _isNeedGet = true;
        _loadPanel.SetActive(true);
    }

    private void ClearLoadLeaderboard()
    {
        _loadShapeType = ShapeType.Null;
        _isLoadLimit = false;
        _loadLeaderboard = null;
    }

    private void Fill()
    {
#if !UNITY_EDITOR
        if (PlayerAccount.IsAuthorized == false)
            return;

        PlayerAccount.GetProfileData((result) =>
        {
            _uniqueID = result.uniqueID;
        });

        _leaderboardPlayerPersonal = null;
        _leaderboardPlayers.Clear();

        Leaderboard.GetEntries(_loadLeaderboard, (result) =>
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
                    _leaderboardPlayerPersonal = _newLeaderboardPlayers;
                }
                
                _leaderboardPlayers.Add(_newLeaderboardPlayers);
            }

            _viewLeaderboard.ConstructLeaderboard(_leaderboardPlayers, _leaderboardPlayerPersonal);
        });
#endif
    }
}

