using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FaceGenerator))]
[RequireComponent(typeof(SpamerBlocks))]
public class FaceController : MonoBehaviour
{
    [SerializeField] private ViewFaceController _viewFaceController;
    [SerializeField] private SavingLoading _savingLoading;
    [SerializeField] private AnimationFaceMove _animationFaceMove;

    private readonly FaceData _faceData = new FaceData();
    private readonly FaceMover _faceMover = new FaceMover();
    
    private ShapeType _shapeType;
    private List<Face> _faces;
    private Face _upFace;
    private Face _downFace;
    private FaceGenerator _faceGenerator;
    private SpamerBlocks _spamerBlocks;

    public IReadOnlyList<Face> Faces => _faces;
    public Face ActiveFace => Faces[0];
    public ShapeType ShapeType => _shapeType;
    public Face UpFace => _upFace;
    public Face DownFace => _downFace;

    private void Awake()
    {
        _faceGenerator = GetComponent<FaceGenerator>();
        _spamerBlocks = GetComponent<SpamerBlocks>();
    }

    public void Init(ShapeType shapeType, List<int> blockValues = null)
    {
        _shapeType = shapeType;
        _upFace = null;
        _downFace = null;

        switch (shapeType)
        {
            case ShapeType.Classic:
                _faces = _faceGenerator.CreateClassic();
                break;
            case ShapeType.Line:
                _faces = _faceGenerator.CreateLine();
                break;
            case ShapeType.Cub:
                _faces = _faceGenerator.CreateCub(out _upFace, out _downFace);
                break;
        }

        _viewFaceController.Init(shapeType);

        SetBlocksStart(blockValues);
    }

    public void SetActivFace(Face face)
    {
        if(face == UpFace)
        {
            MoveDown(false);
        }

        if(face == DownFace)
        {
            MoveUp(false);
        }

        while (face != ActiveFace)
        {
            MoveLeft(false);
        }

        UpdateViewFaceController();
    }

    public void MoveLeft(bool isAnimation = true)
    {
        if (_animationFaceMove.IsMove)
            return;

        _faceMover.MoveLeft(_faces, ref _upFace, ref _downFace);
        SetViewFace();

        if (isAnimation)
        {
            if (_animationFaceMove.IsMove)
                return;

            if (_shapeType == ShapeType.Cub)
            {
                _animationFaceMove.MoveLeftCub();
            }
            else
            {
                _animationFaceMove.MoveLeftLine();
            }
        }
    }

    public void MoveRight(bool isAnimation = true)
    {
        if (_animationFaceMove.IsMove)
            return;

        _faceMover.MoveRight(_faces, ref _upFace, ref _downFace);
        SetViewFace();

        if (isAnimation)
        {
            if (_shapeType == ShapeType.Cub)
            {
                _animationFaceMove.MoveRightCub();
            }
            else
            {
                _animationFaceMove.MoveRightLine();
            }
        }
    }

    public void MoveUp(bool isAnimation = true)
    {
        if (_animationFaceMove.IsMove)
            return;

        _faceMover.MoveUp(_faces, ref _upFace, ref _downFace);
        SetViewFace();

        if (isAnimation)
        {
            _animationFaceMove.MoveUpCub();
        }
    }

    public void MoveDown(bool isAnimation = true)
    {
        if (_animationFaceMove.IsMove)
            return;

        _faceMover.MoveDown(_faces, ref _upFace, ref _downFace);
        SetViewFace();

        if (isAnimation)
        {
            _animationFaceMove.MoveDownCub();
        }
    }

    public void UpdateViewFaceController()
    {
        _viewFaceController.UpdateViewFaces();
    }

    public FaceData GetFaceData()
    {
        _faceData.SetData(this); 
        return _faceData;
    }

    private void SetViewFace()
    {
        _viewFaceController.SetFaces(Faces, UpFace, DownFace);

        _savingLoading.CreateSaveRequest();
    }

    private void SetBlocksStart(List<int> blockValues)
    {
        if (blockValues == null)
        {
            _spamerBlocks.SpamBlocksStart();
        }
        else
        {
            SetBlockValues(blockValues);
        }

        SetViewFace();
        UpdateViewFaceController();
    }

    private void SetBlockValues(List<int> cellValues)
    {
        foreach (Face face in Faces)
        {
            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(face.CellEdge - 1)))
            {
                if (cellValues.Count > 0)
                {
                    if (cellValues[0] != 0)
                    {
                        face.GetCell(valueIJ.I, valueIJ.J).SetBlock(new Block(cellValues[0]));
                    }

                    cellValues.RemoveAt(0);
                }
            }

            //for (int i = 0; i < face.CellEdge; i++) 
            //{
            //    for (int j = 0; j < face.CellEdge; j++)
            //    {
            //        if (cellValues.Count > 0)
            //        {
            //            if (cellValues[0] != 0) 
            //            {
            //                face.GetCell(i, j).SetBlock(new Block(cellValues[0]));
            //            }
            //
            //            cellValues.RemoveAt(0);
            //        }
            //    }
            //}
        }
    }
}