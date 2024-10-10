using Main;
using Main.LimitingMover;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace View
{
    [RequireComponent(typeof(ViewFaceGenerator))]
    public class ViewFaceController : MonoBehaviour
    {
        private const int IndexActiveViewFace = 0;
        private const int IndexRightViewFace = 1;
        private const int IndexLeftViewFace = 2;
        private const int IndexRearViewFace = 3;
        private const int IndexUpViewFace = 4;
        private const int IndexDownViewFace = 5;
        private const int IndexMovingRightViewFace = 3;
        private const int IndexMovingLeftViewFace = 4;
        private const int IndexActiveFace = 0;
        private const int IndexRightFace = 1;
        private const int IndexMovingRightFace = 2;
        private const int IndexRearFace = 2;

        [SerializeField] private LimitingMovements _limitingMovements;

        private ViewFaceGenerator _generatorViewFace;
        private ViewFace _activeViewFace;
        private ViewFace _rightViewFace;
        private ViewFace _leftViewFace;
        private ViewFace _movingRightViewFace;
        private ViewFace _movingLeftViewFace;
        private ViewFace _rearViewFace;
        private ViewFace _upViewFace;
        private ViewFace _downViewFace;
        private List<ViewFace> _viewFaceList;

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
                    _viewFaceList = _generatorViewFace.CreateLine(true);
                    break;
                case ShapeType.Line:
                    _viewFaceList = _generatorViewFace.CreateLine();
                    break;
                case ShapeType.Cub:
                    _viewFaceList = _generatorViewFace.CreateCub();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _activeViewFace = _viewFaceList[IndexActiveViewFace];

            if (shapeType != ShapeType.Classic)
            {
                _rightViewFace = _viewFaceList[IndexRightViewFace];
                _leftViewFace = _viewFaceList[IndexLeftViewFace];

                if (shapeType == ShapeType.Cub)
                {
                    _rearViewFace = _viewFaceList[IndexRearViewFace];
                    _upViewFace = _viewFaceList[IndexUpViewFace];
                    _downViewFace = _viewFaceList[IndexDownViewFace];
                }
                else
                {
                    _movingRightViewFace = _viewFaceList[IndexMovingRightViewFace];
                    _movingLeftViewFace = _viewFaceList[IndexMovingLeftViewFace];
                }
            }
        }

        public void SetFaces(IReadOnlyList<Face> faces, Face upFace, Face downFace)
        {
            _activeViewFace.SetFace(faces[IndexActiveFace]);

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
}
