using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PointsConfigProvider _pointsConfigProvider;

    public event Action ChangedCoins;
    public event Action<int> ChangedPoints;

    private PlayerData _playerData = new PlayerData();
    private int _points;
    private int _coins = 10;

    public int Points => _points;
    public int Coins => _coins;

    private void Start()
    {
        ChangedCoins.Invoke();
    }

    public void AddPoints(CellType cellType)
    {
        SetPoints(_points + _pointsConfigProvider.GetColor(cellType));
    }

    public void SetPoints(int points)
    {
        _points = points;
        ChangedPoints?.Invoke(_points);
    }

    public void —hange—oins(int coins)
    {
        if(_coins + coins < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        SetCoins(_coins + coins);
    }

    public PlayerData GetPlayerData()
    {
        _playerData.SetData(_points, _coins);
        return _playerData;
    }

    private void SetCoins(int coins)
    {
        _coins = coins;
        ChangedCoins?.Invoke();
    }
}
