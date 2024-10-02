using System;
using System.Collections.Generic;
using UnityEngine;

public class CellViewConfigProvider : MonoBehaviour
{
    [SerializeField] private CellViewDatasList _cellViewDatasList;

    private Dictionary<CellType, Color> _colorByCellType;

    private void Awake()
    {
        _colorByCellType = new Dictionary<CellType, Color>();

        foreach (CellViewData cellViewData in _cellViewDatasList.List)
        {
            _colorByCellType.Add(cellViewData.Type, cellViewData.Color);
        }
    }

    public Color GetColor(CellType cellType)
    {
        if (_colorByCellType.ContainsKey(cellType))
            return _colorByCellType[cellType];

        throw new Exception($"Color for cellType {cellType} does not exist!");
    }
}
