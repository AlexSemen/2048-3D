using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PointsDatasList", menuName = "Data/PointsDatasList")]
public class PointsCellDatasList : ScriptableObject
{
    [SerializeField] private List<PointsData> _cells;

    public IReadOnlyList<PointsData> Cells => _cells;
}
