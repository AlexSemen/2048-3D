using System;
using UnityEngine;

[Serializable]
public class PointsData
{
    [SerializeField] private CellType _type;
    [SerializeField] private int _points;

    public CellType Type => _type;
    public int Points => _points;
}
