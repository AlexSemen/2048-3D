using Loop;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    [RequireComponent(typeof(FaceController))]
    public class SpamerBlocks : MonoBehaviour
    {
        private const int MinQuantitySpamBlocks = 1;
        private const int QuantityStartBlocks = 2;
        private const int Max—hanceSpamBloksRandomFace = 100;
        private const int MinIndexSpamBloksRandomFace = 1;

        private readonly List<Cell> _freeCells = new List<Cell>();
        private readonly int[] _spawnBlockMeaning = new int[2] { 4, 8 };

        [SerializeField] private int _chanceSpamBlokRandomFace = 25;

        private FaceController _faceController;
        private Cell _currentCell;

        private void Awake()
        {
            _faceController = GetComponent<FaceController>();
        }

        private void OnValidate()
        {
            _chanceSpamBlokRandomFace = Mathf.Clamp(_chanceSpamBlokRandomFace, 0, Max—hanceSpamBloksRandomFace);
        }

        public void TrySpamBlok(Face face, int quantity = MinQuantitySpamBlocks)
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

            if (Random.Range(0, Max—hanceSpamBloksRandomFace) <= _chanceSpamBlokRandomFace)
            {
                for (int i = MinIndexSpamBloksRandomFace; i < _faceController.Faces.Count; i++)
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
            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(face.CellEdge - 1)))
            {
                if (_freeCells.Contains(face.GetCell(valueIJ.I, valueIJ.J)) == false && face.GetCell(valueIJ.I, valueIJ.J).Block == null)
                {
                    _freeCells.Add(face.GetCell(valueIJ.I, valueIJ.J));
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
}
