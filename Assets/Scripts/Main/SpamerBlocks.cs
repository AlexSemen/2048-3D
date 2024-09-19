using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FaceController))]
public class SpamerBlocks : MonoBehaviour
{
    [SerializeField] private int _chanceSpamBlokRandomFace = 25;

    private const int MinQuantitySpamBlock = 1;
    private const int QuantityStartBlocks = 2;
    private const int Max—hanceSpamBlokRandomFace = 100;
    private const int MinIndexSpamBlokRandomFace = 1;
    
    private readonly List<Cell> _freeCells = new List<Cell>();
    private readonly int[] _spawnBlockMeaning = new int[2] { 4, 8 };

    private FaceController _faceController;
    private Cell _currentCell;

    private void Awake()
    {
        _faceController = GetComponent<FaceController>();
    }

    private void OnValidate()
    {
        _chanceSpamBlokRandomFace = Mathf.Clamp(_chanceSpamBlokRandomFace, 0, Max—hanceSpamBlokRandomFace);
    }

    public void TrySpamBlok(Face face, int quantity = MinQuantitySpamBlock)
    {
        _freeCells.Clear();

        AddFreeCells(face);

        if (_freeCells.Count < quantity)
        {
            quantity = _freeCells.Count;
        }

        for (int i = 0; i < quantity; i++)
        {
            SpamBlock();
        }
    }

    public void TrySpamBlokRandomFace()
    {
        _freeCells.Clear();

        if (Random.Range(0, Max—hanceSpamBlokRandomFace) <= _chanceSpamBlokRandomFace)
        {
            for (int i = MinIndexSpamBlokRandomFace; i < _faceController.Faces.Count; i++)
            {
                AddFreeCells(_faceController.Faces[i]);
            }

            if (_faceController.ShapeType == ShapeType.Cub)
            {
                AddFreeCells(_faceController.UpFace);
                AddFreeCells(_faceController.DownFace);
            }

            RemoveFreeCells(_faceController.ActiveFace);

            SpamBlock();
        }
    }

    public void SpamBlocksStart()
    {
        foreach (Face face in _faceController.Faces)
        {
            TrySpamBlok(face, QuantityStartBlocks);
        }

        if (_faceController.ShapeType == ShapeType.Cub)
        {
            TrySpamBlok(_faceController.UpFace, QuantityStartBlocks);
            TrySpamBlok(_faceController.DownFace, QuantityStartBlocks);
        }
    }

    private void AddFreeCells(Face face)
    {
        for (int i = 0; i < face.CellEdge; i++)
        {
            for (int j = 0; j < face.CellEdge; j++)
            {
                if (_freeCells.Contains(face.GetCell(i, j)) == false && face.GetCell(i, j).Block == null)
                {
                    _freeCells.Add(face.GetCell(i, j));
                }
            }
        }
    }

    private void RemoveFreeCells(Face face)
    {
        for (int i = 0; i < face.CellEdge; i++)
        {
            for (int j = 0; j < face.CellEdge; j++)
            {
                if (_freeCells.Contains(face.GetCell(i, j)))
                {
                    _freeCells.Remove(face.GetCell(i, j));
                    j--;
                }
            }
        }
    }

    private void SpamBlock()
    {
        if (_freeCells.Count != 0)
        {
            _currentCell = _freeCells[Random.Range(0, _freeCells.Count)];

            _currentCell.SetBlock(new Block(_spawnBlockMeaning[Random.Range(0, _spawnBlockMeaning.Length)]));

            _freeCells.Remove(_currentCell);
        }
    }
}
