using Loop;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main {
    public class Face
    {
        private const int QuantityFaceOfCube = 4;
        private const int IndexActiveFaceCub = 0;
        private const int IndexRightFaceCub = 1;
        private const int IndexRearFaceCub = 2;
        private const int IndexLeftFaceCub = 3;

        private readonly int _ñellEdge = 4;
        private readonly TurnFace _turnFace = new TurnFace();

        private Cell[,] _cells;

        public int CellEdge => _ñellEdge;

        public Face()
        {
            _cells = new Cell[CellEdge, CellEdge];
        }

        public void Init()
        {
            FillEmptyCell();
        }

        public void Init(Face faceLeft)
        {
            FillCellLeft(faceLeft);
            Init();
        }

        public void Init(Face faceLeft, Face faceRight)
        {
            FillCellRight(faceRight);
            Init(faceLeft);
        }

        public void InitUp(IReadOnlyList<Face> faces)
        {
            InitUp2(
                faces,
                true,
                (i) => new Vector2Int(0, i),
                (i) => new Vector2Int(0, CellEdge - i - 1),
                (i) => new Vector2Int(0, CellEdge - i - 1),
                (i) => new Vector2Int(0, i));
        }

        public void InitDown(IReadOnlyList<Face> faces)
        {
            InitUp2(
                faces,
                false,
                (i) => new Vector2Int(CellEdge - 1, CellEdge - i - 1),
                (i) => new Vector2Int(CellEdge - 1, i),
                (i) => new Vector2Int(CellEdge - 1, CellEdge - i - 1),
                (i) => new Vector2Int(CellEdge - 1, i));
        }

        private void InitUp2(
            IReadOnlyList<Face> faces,
            bool isUp,
            Func<int, Vector2Int> leftFaceCellVector2Int,
            Func<int, Vector2Int> rightFaceCellVector2Int,
            Func<int, Vector2Int> rearFaceCellVector2Int,
            Func<int, Vector2Int> activeFaceCellVector2Int)
        {
            if (faces.Count != QuantityFaceOfCube)
            {
                throw new ArgumentOutOfRangeException();
            }

            for (int i = 0; i < CellEdge; i++)
            {
                _cells[i, 0] = faces[IndexLeftFaceCub].GetCell(leftFaceCellVector2Int(i));
                _cells[i, CellEdge - 1] = faces[IndexRightFaceCub].GetCell(rightFaceCellVector2Int(i));
                
                if (isUp)
                {
                    _cells[0, i] = faces[IndexRearFaceCub].GetCell(rearFaceCellVector2Int(i));
                    _cells[CellEdge - 1, i] = faces[IndexActiveFaceCub].GetCell(activeFaceCellVector2Int(i));
                }
                else
                {
                    _cells[0, i] = faces[IndexActiveFaceCub].GetCell(activeFaceCellVector2Int(i));
                    _cells[CellEdge - 1, i] = faces[IndexRearFaceCub].GetCell(rearFaceCellVector2Int(i));
                }
            }

            FillEmptyCell();
        }

        public Cell GetCell(int i, int j)
        {
            return _cells[i, j];
        }

        public Cell GetCell(Vector2Int vector2)
        {
            return GetCell(vector2.x, vector2.y);
        }

        public void ThrowStatusBlock()
        {
            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(CellEdge - 1)))
            {
                _cells[valueIJ.I, valueIJ.J].ThrowStatusBlock();
            }
        }

        public void TurnRight()
        {
            _cells = _turnFace.TurnRight(_cells, CellEdge);
        }

        public void TurnLeft()
        {
            _cells = _turnFace.TurnLeft(_cells, CellEdge);
        }

        public void Invert()
        {
            _cells = _turnFace.Invert(_cells, CellEdge);
        }

        private void FillEmptyCell()
        {

            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(CellEdge - 1)))
            {
                if (_cells[valueIJ.I, valueIJ.J] == null)
                {
                    _cells[valueIJ.I, valueIJ.J] = new Cell();
                }
            }
        }

        private void FillCellLeft(Face faceLeft)
        {
            FillCells(
                faceLeft,
                (i) => new Vector2Int(i, 0),
                (i) => new Vector2Int(i, CellEdge - 1));
        }

        private void FillCellRight(Face faceRight)
        {
            FillCells(
               faceRight,
               (i) => new Vector2Int(i, CellEdge - 1),
               (i) => new Vector2Int(i, 0));
        }

        private void FillCells(Face face, Func<int, Vector2Int> coordinatesCells, Func<int, Vector2Int> coordinatesTargetFaceCells)
        {
            for (int i = 0; i < CellEdge; i++)
            {
                _cells[coordinatesCells(i).x, coordinatesCells(i).y] = face.GetCell(coordinatesTargetFaceCells(i));
            }
        }
    }
}
