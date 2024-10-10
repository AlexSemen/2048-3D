using Loop;
using Main;
using System.Collections.Generic;
using UnityEngine;

namespace Explosives
{
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

            foreach (Face face in faceController.Faces)
            {
                WriteDownCellFromFace(face);
            }

            if (faceController.ShapeType == ShapeType.Cub)
            {
                WriteDownCellFromFace(faceController.UpFace);
                WriteDownCellFromFace(faceController.DownFace);
            }
            
            return _listTargetCell;
        }

        private void WriteDownCellFromFace(Face face)
        {
            _isCellOfFace = false;


            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(face.CellEdge - 1)))
            {
                if (face.GetCell(valueIJ.I, valueIJ.J) == _targetCell)
                {
                    _isCellOfFace = true;
                    _targetX = valueIJ.J;
                    _targetY = valueIJ.I;
                }
            }

            if (_isCellOfFace)
            {
                _minTargetX = (int)Mathf.Clamp(_targetX - _distance, 0, face.CellEdge - 1);
                _maxTargetX = (int)Mathf.Clamp(_targetX + _distance, 0, face.CellEdge - 1);
                _minTargetY = (int)Mathf.Clamp(_targetY - _distance, 0, face.CellEdge - 1);
                _maxTargetY = (int)Mathf.Clamp(_targetY + _distance, 0, face.CellEdge - 1);

                foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(_maxTargetY, _minTargetY),
                    new SettingsLoop(_maxTargetX+1, _minTargetX)))
                {
                    if (_listTargetCell.Contains(face.GetCell(valueIJ.I, valueIJ.J)) == false)
                    {
                        _listTargetCell.Add(face.GetCell(valueIJ.I, valueIJ.J));
                    }
                }
            }
        }
    }
}