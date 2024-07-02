using System;
using UnityEngine;

[Serializable]
public class CellViewData
{
    [field: SerializeField] private CellType _type;
    [field: SerializeField] private Color _color;

    public CellType Type => _type;
    public Color Color => _color;
}
