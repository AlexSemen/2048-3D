using System;
using System.Collections.Generic;
using UnityEngine;
using View;
using View.UI;
using Yandex.SaveLoad.SaveData;

namespace Main.LimitingMover 
{
    [RequireComponent(typeof(ViewLimitingMovements))]
    public class LimitingMovements : MonoBehaviour
    {
        private const int MaxLimitMove = 3;

        private readonly LimitingData _limitingData = new LimitingData();
        private readonly Dictionary<Face, int> _numberMovesOnFace = new Dictionary<Face, int>();
        
        [SerializeField] private FaceController _faceController;
        [SerializeField] private Orientation _limitHalpPanel;

        private bool _isLimitMove = false;
        private ViewLimitingMovements _viewLimitingMovements;

        public Orientation LimitHalpPanel => _limitHalpPanel;

        private void Awake()
        {
            _viewLimitingMovements = GetComponent<ViewLimitingMovements>();
        }

        public void Init(List<int> values = null)
        {
            _isLimitMove = true;
            _viewLimitingMovements.SetActivObject(true);

            if (values == null)
            {
                foreach (Face face in _faceController.Faces)
                {
                    _numberMovesOnFace.Add(face, MaxLimitMove);
                }

                if (_faceController.ShapeType == ShapeType.Cub)
                {
                    _numberMovesOnFace.Add(_faceController.UpFace, MaxLimitMove);
                    _numberMovesOnFace.Add(_faceController.DownFace, MaxLimitMove);
                }
            }
            else
            {
                for (int i = 0; i < _faceController.Faces.Count; i++)
                {
                    _numberMovesOnFace.Add(_faceController.Faces[i], values[i]);
                }

                if (_faceController.ShapeType == ShapeType.Cub)
                {
                    _numberMovesOnFace.Add(_faceController.UpFace, values[values.Count - 2]);
                    _numberMovesOnFace.Add(_faceController.DownFace, values[values.Count - 1]);
                }
            }

            UpdateCurrentCanMoveText();
        }

        public void Clear()
        {
            _numberMovesOnFace.Clear();
            _isLimitMove = false;
            _viewLimitingMovements.SetActivObject(false);
        }

        public bool IsCanMoveBlockOnActiveFace()
        {
            if (_isLimitMove == false || _numberMovesOnFace[_faceController.ActiveFace] > 0)
            {
                return true;
            }
            else
            {
                _viewLimitingMovements.StartAnimation();
                LimitHalpPanel.gameObject.SetActive(true);
                return false;
            }
        }

        public void Move()
        {
            if (_isLimitMove == false)
                return;

            foreach (Face face in _faceController.Faces)
            {
                if (face != _faceController.ActiveFace)
                {
                    TryAddCanMove(face);
                }
                else
                {
                    if (_numberMovesOnFace[face] > 0)
                    {
                        _numberMovesOnFace[face]--;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
            }

            if (_faceController.ShapeType == ShapeType.Cub)
            {
                TryAddCanMove(_faceController.UpFace);
                TryAddCanMove(_faceController.DownFace);
            }

            _viewLimitingMovements.StartAnimation();
        }

        public void AddCanMoveActivFace()
        {
            _numberMovesOnFace[_faceController.ActiveFace]++;
            UpdateCurrentCanMoveText();
        }

        public void UpdateCurrentCanMoveText()
        {
            if (_numberMovesOnFace.Count > 0)
            {
                _viewLimitingMovements.SetCurrentCanMoveText(_numberMovesOnFace[_faceController.ActiveFace].ToString());
            }
        }

        public LimitingData GetLimitingData()
        {
            _limitingData.SetData(_isLimitMove, _numberMovesOnFace, _faceController);
            return _limitingData;
        }

        public void TurnOnView()
        {
            if (_isLimitMove)
            {
                _viewLimitingMovements.SetActivObject(true);
            }
        }

        public void TurnOffView()
        {
            _viewLimitingMovements.SetActivObject(false);
        }

        private void TryAddCanMove(Face face)
        {
            if (_numberMovesOnFace[face] < MaxLimitMove)
            {
                _numberMovesOnFace[face]++;
            }
        }
    }
}
