using System;
using UnityEngine;

namespace Data.CellData.PointsData
{
    [Serializable]
    public class CellViewData
    {
        [field: SerializeField] private CellType _type;
        [field: SerializeField] private Color _color;

        public CellType Type => _type;
        public Color Color => _color;
    }
}
