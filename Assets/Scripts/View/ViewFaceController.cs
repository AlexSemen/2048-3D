using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ViewFaceGenerator))]
public class ViewFaceController : MonoBehaviour
{
    [SerializeField] private LimitingMovements _limitingMovements;

    private const int IndexActivViewFace = 0;
    private const int IndexRightViewFace = 1;
    private const int IndexLeftViewFace = 2;
    private const int IndexRearViewFace = 3;
    private const int IndexUpViewFace = 4;
    private const int IndexDownViewFace = 5;
    private const int IndexMovingRightViewFace = 3;
    private const int IndexMovingLeftViewFace = 4;
    private const int IndexActivFace = 0;
    private const int IndexRightFace = 1;
    private const int IndexMovingRightFace = 2;
    private const int IndexRearFace = 2;

    private ViewFaceGenerator _generatorViewFace;
    private ViewFace _activeViewFace;
    private ViewFace _rightViewFace;
    private ViewFace _leftViewFace;
    private ViewFace _movingRightViewFace;
    private ViewFace _movingLeftViewFace;
    private ViewFace _rearViewFace;
    private ViewFace _upViewFace;
    private ViewFace _downViewFace;
    private List<ViewFace> _viewFaces;

    public ViewFace ActiveViewFace => _activeViewFace;

    private void Awake()
    {
        _generatorViewFace = GetComponent<ViewFaceGenerator>();
    }

    public void Init(ShapeType shapeType)
    {
        Clear();

        switch (shapeType)
        {
            case ShapeType.Classic:
                _viewFaces = _generatorViewFace.CreateLine(true);
                break;
            case ShapeType.Line:
                _viewFaces = _generatorViewFace.CreateLine();
                break;
            case ShapeType.Cub:
                _viewFaces = _generatorViewFace.CreateCub();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _activeViewFace = _viewFaces[IndexActivViewFace];

        if (shapeType != ShapeType.Classic)
        {
            _rightViewFace = _viewFaces[IndexRightViewFace];
            _leftViewFace = _viewFaces[IndexLeftViewFace];

            if (shapeType == ShapeType.Cub)
            {
                _rearViewFace = _viewFaces[IndexRearViewFace];
                _upViewFace = _viewFaces[IndexUpViewFace];
                _downViewFace = _viewFaces[IndexDownViewFace];
            }
            else
            {
                _movingRightViewFace = _viewFaces[IndexMovingRightViewFace];
                _movingLeftViewFace = _viewFaces[IndexMovingLeftViewFace];
            }
        }
    }

    public void SetFaces(IReadOnlyList<Face> faces, Face upFace, Face downFace)
    {
        _activeViewFace.SetFace(faces[IndexActivFace]);

        _rightViewFace?.SetFace(faces[IndexRightFace]);
        _leftViewFace?.SetFace(faces[faces.Count - 1]);

        _movingRightViewFace?.SetFace(faces[IndexMovingRightFace]);
        _movingLeftViewFace?.SetFace(faces[faces.Count - 2]);

        if (upFace != null && downFace != null)
        {
            _rearViewFace.SetFace(faces[IndexRearFace]);
            _upViewFace.SetFace(upFace);
            _downViewFace.SetFace(downFace);
        }
    }

    public void UpdateViewFaces()
    {
        _activeViewFace?.DisplayCell();
        _rightViewFace?.DisplayCell();
        _leftViewFace?.DisplayCell();

        _rearViewFace?.DisplayCell();
        _upViewFace?.DisplayCell();
        _downViewFace?.DisplayCell();

        _movingRightViewFace?.DisplayCell();
        _movingLeftViewFace?.DisplayCell();

        _limitingMovements.UpdateCurrentCanMoveText();
        _limitingMovements.LimitHalpPanel.gameObject.SetActive(false);
    }

    private void Clear()
    {
        _activeViewFace = null;
        _rightViewFace = null;
        _leftViewFace = null;

        _rearViewFace = null;
        _upViewFace = null;
        _downViewFace = null;

        _movingRightViewFace = null;
        _movingLeftViewFace = null;
    }
}
