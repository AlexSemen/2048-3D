using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ViewLimitingMovements))]
public class LimitingMovements : MonoBehaviour
{
    [SerializeField] private FaceController _faceController;

    private const int _maxLimitMove = 3;

    private bool _isLimitMove = false;
    private LimitingData _limitingData = new LimitingData();
    private ViewLimitingMovements _viewLimitingMovements;
    private Dictionary<Face, int> _faceMoves = new Dictionary<Face, int>();
    

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
                _faceMoves.Add(face, _maxLimitMove);
            }

            if (_faceController.ShapeType == ShapeType.Cub)
            {
                _faceMoves.Add(_faceController.UpFace, _maxLimitMove);
                _faceMoves.Add(_faceController.DownFace, _maxLimitMove);
            }
        }
        else
        {
            for (int i = 0; i < _faceController.Faces.Count; i++)
            {
                _faceMoves.Add(_faceController.Faces[i], values[i]);
            }

            if (_faceController.ShapeType == ShapeType.Cub)
            {
                _faceMoves.Add(_faceController.UpFace, values[values.Count - 2]);
                _faceMoves.Add(_faceController.DownFace, values[values.Count - 1]);
            }
        }

        UpdateCurrentCanMoveText();
    }

    public void Clear()
    {
        _faceMoves.Clear();
        _isLimitMove = false;
        _viewLimitingMovements.SetActivObject(false);
    }

    public bool IsCanMoveBlockOnActiveFace()
    {
        if (_isLimitMove == false || _faceMoves[_faceController.ActiveFace] > 0)
        {
            return true;
        }
        else
        {
            _viewLimitingMovements.StartAnimation();
            return false;
        }
    }

    public void Move() 
    {
        if(_isLimitMove == false) 
            return;

        foreach (Face face in _faceController.Faces)
        {
            if(face != _faceController.ActiveFace)
            {
                TryAddCanMove(face);
            }
            else
            {
                if (_faceMoves[face] > 0)
                {
                    _faceMoves[face]--;
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
        _faceMoves[_faceController.ActiveFace]++;
        UpdateCurrentCanMoveText();
    }

    public void UpdateCurrentCanMoveText()
    {
        if (_faceMoves.Count > 0)
        {
            _viewLimitingMovements.SetCurrentCanMoveText(_faceMoves[_faceController.ActiveFace].ToString());
        }
    }

    public LimitingData GetLimitingData()
    {
        _limitingData.SetData(_isLimitMove, _faceMoves, _faceController);
        return _limitingData;
    }

    public void TurnOnView()
    {
        if(_isLimitMove)
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
        if (_faceMoves[face] < _maxLimitMove)
        {
            _faceMoves[face]++;
        }
    }
}
