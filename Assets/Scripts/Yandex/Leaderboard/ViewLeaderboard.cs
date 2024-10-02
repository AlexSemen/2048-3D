using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;
    [SerializeField] private LeaderboardElement _leaderboardElementPersonal;
    [SerializeField] private int _quantityPlayersShown = 9;

    private const string NullRank = "-";
    private const string NullName = "-----";
    private const string NullScore = "---";

    private readonly List<LeaderboardElement> _spawnedElements = new List<LeaderboardElement>();

    private LeaderboardPlayer _leaderboardPlayerNull;

    private void Awake()
    {
        _leaderboardPlayerNull = new LeaderboardPlayer(NullRank, NullName, NullScore);
    }

    public void ConstructLeaderboard(List<LeaderboardPlayer> leaderboardPlayers, LeaderboardPlayer leaderboardPlayerPersonal)
    {
        ClearLeaderboard();

        for (int i = 0; i < _quantityPlayersShown; i++)
        {
            LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _container);

            if (i < leaderboardPlayers.Count)
            {
                leaderboardElementInstance.Initialize(leaderboardPlayers[i]);
                if(leaderboardPlayers[i] == leaderboardPlayerPersonal)
                {
                    leaderboardElementInstance.TurnOnFrame();
                }
            }
            else
            {
                leaderboardElementInstance.Initialize(_leaderboardPlayerNull);
            }

            _spawnedElements.Add(leaderboardElementInstance);
        }

        if (leaderboardPlayerPersonal != null)
        {
            _leaderboardElementPersonal.Initialize(leaderboardPlayerPersonal);
        }
        else
        {
            _leaderboardElementPersonal.Initialize(_leaderboardPlayerNull);
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _spawnedElements)
        {
            Destroy(element.gameObject);
        }

        _spawnedElements.Clear();
    }
}
