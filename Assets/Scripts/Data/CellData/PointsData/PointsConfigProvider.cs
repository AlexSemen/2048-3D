using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.CellData.PointsData
{
    public class PointsConfigProvider : MonoBehaviour
    {
        [SerializeField] private PointsCellDataList _pointsCellDatasList;

        private Dictionary<CellType, int> _pointsByCellType;

        private void Awake()
        {
            _pointsByCellType = new Dictionary<CellType, int>();

            foreach (PointsData pointsData in _pointsCellDatasList.List)
            {
                _pointsByCellType.Add(pointsData.Type, pointsData.Points);
            }
        }

        public int GetColor(CellType cellType)
        {
            if (_pointsByCellType.ContainsKey(cellType))
                return _pointsByCellType[cellType];

            throw new Exception($"Point for cellType {cellType} does not exist!");
        }
    }
}
