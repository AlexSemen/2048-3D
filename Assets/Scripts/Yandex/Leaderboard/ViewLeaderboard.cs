using System;
using System.Collections.Generic;
using UnityEngine;

public class ViewLeaderboard : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private LeaderboardElement _leaderboardElementPrefab;
    [SerializeField] private LeaderboardElement _leaderboardElementPersonal;
    [SerializeField] private int quantityPlayersShown = 9;

    public event Action Disable;

    private const string _nullRank = "-";
    private const string _nullName = "-----";
    private const string _nullScore = "---";

    private List<LeaderboardElement> _spawnedElements = new List<LeaderboardElement>();
    private LeaderboardPlayer _leaderboardPlayerNull;

    private void Awake()
    {
        _leaderboardPlayerNull = new LeaderboardPlayer(_nullRank, _nullName, _nullScore);
    }

    private void OnDisable()
    {
        Disable?.Invoke();
    }

    public void ConstructLeaderboard(List<LeaderboardPlayer> leaderboardPlayers, LeaderboardPlayer leaderboardPlayerPersonal)
    {
        ClearLeaderboard();

        for (int i = 0; i < quantityPlayersShown; i++)
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
