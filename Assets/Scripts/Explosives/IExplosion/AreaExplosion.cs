using System.Collections.Generic;
using UnityEngine;

public class AreaExplosion : IExplosion
{
    private const int _distance = 1;
    
    private readonly List<Cell> _listTargetCell = new List<Cell>();
    
    private Cell _targetCell;
    private bool _isCellOfFace;
    private int _targetX;
    private int _targetY;
    private int _minTargetX;
    private int _maxTargetX;
    private int _minTargetY;
    private int _maxTargetY;

    public List<Cell> GetCellTarget(Cell cell, FaceController faceController)
    {
        _listTargetCell.Clear();

        _targetCell = cell;

        foreach(Face face in faceController.Faces)
        {
            WriteDownCellFromFace(face);
        }

        if(faceController.ShapeType == ShapeType.Cub) 
        {
            WriteDownCellFromFace(faceController.UpFace);
            WriteDownCellFromFace(faceController.DownFace);
        }

        return _listTargetCell;
    }

    private void WriteDownCellFromFace(Face face)
    {
        _isCellOfFace = false;

        for (int i = 0; i < face.CellEdge; i++)
        {
            for (int j = 0; j < face.CellEdge; j++)
            {
                if (face.GetCell(i, j) == _targetCell)
                {
                    _isCellOfFace = true;
                    _targetX = j;
                    _targetY = i;
                }
            }
        }

        if (_isCellOfFace)
        {
            _minTargetX = (int)Mathf.Clamp(_targetX - _distance, 0, face.CellEdge - 1);
            _maxTargetX = (int)Mathf.Clamp(_targetX + _distance, 0, face.CellEdge - 1);
            _minTargetY = (int)Mathf.Clamp(_targetY - _distance, 0, face.CellEdge - 1);
            _maxTargetY = (int)Mathf.Clamp(_targetY + _distance, 0, face.CellEdge - 1);

            for (int i = _minTargetY; i <= _maxTargetY; i++)
            {
                for (int j = _minTargetX; j <= _maxTargetX; j++)
                {
                   if(_listTargetCell.Contains(face.GetCell(i, j)) == false)
                   {
                        _listTargetCell.Add(face.GetCell(i, j));
                   }
                }
            }
        }
    }
}