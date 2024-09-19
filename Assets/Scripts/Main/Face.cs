using System;
using System.Collections.Generic;

public class Face
{
    private const int QuantityFaceOfCube = 4;
    private const int IndexActivFaceCub = 0;
    private const int IndexRightFaceCub = 1;
    private const int IndexRearFaceCub = 2;
    private const int IndexLeftFaceCub = 3;

    private readonly TurnFace _turnFace = new TurnFace();

    private const int _ñellEdge = 4;
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
        FillEmptyCell();
    }

    public void Init(Face faceLeft, Face faceRight)
    {
        FillCellLeft(faceLeft);
        FillCellRight(faceRight);
        FillEmptyCell();
    }

    public void InitUp(IReadOnlyList<Face> faces)
    {
        if (faces.Count != QuantityFaceOfCube)
        {
            throw new ArgumentOutOfRangeException();
        }

        for (int i = 0; i < CellEdge; i++)
        {
            _cells[i, 0] = faces[IndexLeftFaceCub].GetCell(0, i);
        }

        for (int i = 0; i < CellEdge; i++)
        {
            _cells[i, CellEdge - 1] = faces[IndexRightFaceCub].GetCell(0, CellEdge - i - 1);
        }

        for (int i = 0; i < CellEdge; i++)
        {
            _cells[0, i] = faces[IndexRearFaceCub].GetCell(0, CellEdge - i - 1);
        }

        for (int i = 0; i < CellEdge; i++)
        {
            _cells[CellEdge - 1, i] = faces[IndexActivFaceCub].GetCell(0, i);
        }

        FillEmptyCell();
    }

    public void InitDown(IReadOnlyList<Face> faces)
    {
        if (faces.Count != QuantityFaceOfCube)
        {
            throw new ArgumentOutOfRangeException();
        }

        for (int i = 0; i < CellEdge; i++)
        {
            _cells[i, 0] = faces[IndexLeftFaceCub].GetCell(CellEdge - 1, CellEdge - i - 1);
        }

        for (int i = 0; i < CellEdge; i++)
        {
            _cells[i, CellEdge - 1] = faces[IndexRightFaceCub].GetCell(CellEdge - 1, i);
        }

        for (int i = 0; i < CellEdge; i++)
        {
            _cells[0, i] = faces[IndexActivFaceCub].GetCell(CellEdge - 1, i);
        }

        for (int i = 0; i < CellEdge; i++)
        {
            _cells[CellEdge - 1, i] = faces[IndexRearFaceCub].GetCell(CellEdge - 1, CellEdge - i - 1);
        }

        FillEmptyCell();
    }

    public Cell GetCell(int i, int j)
    {
        return _cells[i, j];
    }

    public void ThrowStatusBlock()
    {
        for (int i = 0; i < CellEdge; i++)
        {
            for (int j = 0; j < CellEdge; j++)
            {
                _cells[i, j].ThrowStatusBlock();
            }
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
        for (int i = 0; i < CellEdge; i++)
        {
            for (int j = 0; j < CellEdge; j++)
            {
                if (_cells[i, j] == null)
                {
                    _cells[i, j] = new Cell();
                }
            }
        }
    }

    private void FillCellLeft(Face faceLeft)
    {
        for (int i = 0; i < CellEdge; i++)
        {
            _cells[i, 0] = faceLeft.GetCell(i, CellEdge - 1);
        }
    }
    
    private void FillCellRight(Face faceRight)
    {
        for (int i = 0; i < CellEdge; i++)
        {
            _cells[i, CellEdge - 1] = faceRight.GetCell(i, 0);
        }
    }
}
