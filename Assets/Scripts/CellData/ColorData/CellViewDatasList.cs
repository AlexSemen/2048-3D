using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellViewDatasList", menuName = "Data/CellViewDatasList")]
public class CellViewDatasList : ScriptableObject
{
    [SerializeField] private List<CellViewData> _cells;

    public IReadOnlyList<CellViewData> Cells => _cells;
}
