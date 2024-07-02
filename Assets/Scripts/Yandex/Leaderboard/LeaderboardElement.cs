using TMPro;
using UnityEngine;

public class LeaderboardElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerName; 
    [SerializeField] private TMP_Text _playerRank;
    [SerializeField] private TMP_Text _playerScore;

    public void Initialize(LeaderboardPlayer player) 
    {
        _playerName.text = player.Name;
        _playerRank.text = player.Rank;
        _playerScore.text = player.Score; 
    }
}
