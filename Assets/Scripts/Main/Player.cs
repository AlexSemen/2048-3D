using System;
using UnityEngine;
using CellData;
using CellData.PointsData;
using Yandex.SaveLoad.SaveData;

namespace Main {
    public class Player : MonoBehaviour
    {
        private const int PointsToGetCoin = 100;
        
        private readonly PlayerData _playerData = new PlayerData();

        [SerializeField] private PointsConfigProvider _pointsConfigProvider;

        public event Action ChangedCoins;
        public event Action<int> ChangedPoints;

        private int _pointsForWhichCoinsWereReceived = 0;
        private int _points = 0;
        private int _coins = 100;

        public int Coins => _coins;

        private void Start()
        {
            ChangedCoins?.Invoke();
        }

        public void AddPoints(CellType cellType)
        {
            SetPoints(_points + _pointsConfigProvider.GetColor(cellType));

            if (_points - _pointsForWhichCoinsWereReceived >= PointsToGetCoin)
            {
                _pointsForWhichCoinsWereReceived += PointsToGetCoin;
                _coins++;
                ChangedCoins?.Invoke();
            }
        }

        public void StartPoints(int points)
        {
            _pointsForWhichCoinsWereReceived = points / PointsToGetCoin * PointsToGetCoin;

            SetPoints(points);
        }

        public void —hange—oins(int coins)
        {
            if (_coins + coins < 0)
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

        private void SetPoints(int points)
        {
            _points = points;
            ChangedPoints?.Invoke(_points);
        }

        private void SetCoins(int coins)
        {
            _coins = coins;
            ChangedCoins?.Invoke();
        }
    }
}
