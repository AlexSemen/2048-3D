using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ViewCellGenerator))]
public class ViewFaceGenerator : MonoBehaviour
{
    [SerializeField] private ViewFace _prefabViewFace;
    [SerializeField] private AnimationBlockMove _animationBlockMove;

    private const int IndexActiveViewFace = 0;
    private const float AngleRotationCub = 90;
    private const float AngleRotationLine = 20.5f;
    private const float PositionLineZ = 15;
    private const float PositionCubZ = 1.75f;
    
    private ViewCellGenerator _viewCellGenerator;
    private List<ViewFace> _viewFaces = new List<ViewFace>();
    private Transform _transform;
    private Transform _spawnPoint;

    private void Awake()
    {
        _transform = transform;
        _spawnPoint = new GameObject("SpawnViewFacePoint").transform;
        _spawnPoint.SetParent(_transform);
        _viewCellGenerator = GetComponent<ViewCellGenerator>();
    }

    public List<ViewFace> CreateLine(bool classic = false)
    {
        ClearViewFace();
        _transform.position = new Vector3(0, 0, PositionLineZ);
        _spawnPoint.position = Vector3.zero;

        _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));

        if (classic == false)
        {
            _transform.rotation = Quaternion.Euler(0, -AngleRotationLine, 0);
            _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));
            _transform.rotation = Quaternion.Euler(0, AngleRotationLine, 0);
            _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));

            _transform.rotation = Quaternion.Euler(0, -AngleRotationLine * 2, 0);
            _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));
            _transform.rotation = Quaternion.Euler(0, AngleRotationLine * 2, 0);
            _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));

            _transform.rotation = Quaternion.identity;
        }

        InitViewFaces();
        SetParentViewFaces();

        return _viewFaces;
    }

    public List<ViewFace> CreateCub()
    {
        ClearViewFace();
        _transform.position = new Vector3(0, 0, PositionCubZ);
        _spawnPoint.position = Vector3.zero;

        _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));
        _transform.rotation = Quaternion.Euler(0, -AngleRotationCub, 0);
        _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));
        _transform.rotation = Quaternion.Euler(0, AngleRotationCub, 0);
        _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));
        _transform.rotation = Quaternion.Euler(0, AngleRotationCub * 2, 0);
        _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));
        _transform.rotation = Quaternion.Euler(AngleRotationCub, 0, 0);
        _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));
        _transform.rotation = Quaternion.Euler(-AngleRotationCub, 0, 0);
        _viewFaces.Add(Instantiate(_prefabViewFace, _spawnPoint.position, _spawnPoint.rotation));

        _transform.rotation = Quaternion.identity;

        InitViewFaces();
        SetParentViewFaces();

        return _viewFaces;
    }

    private void InitViewFaces()
    {
        foreach (ViewFace viewFace in _viewFaces)
        {
            if (viewFace == _viewFaces[IndexActiveViewFace])
            {
                viewFace.Init(_viewCellGenerator.Init(viewFace.transform, true), _animationBlockMove);
            }
            else
            {
                viewFace.Init(_viewCellGenerator.Init(viewFace.transform), _animationBlockMove);
            }
        }
    }

    private void SetParentViewFaces()
    {
        foreach (ViewFace viewFace in _viewFaces)
        {
            viewFace.transform.SetParent(_transform);
        }
    }

    private void ClearViewFace()
    {
        _transform.position = Vector3.zero;
        _transform.rotation = Quaternion.identity;

        foreach (ViewFace viewFace in _viewFaces)
        {
            Destroy(viewFace.gameObject);
        }

        _viewFaces.Clear();
    }
}
