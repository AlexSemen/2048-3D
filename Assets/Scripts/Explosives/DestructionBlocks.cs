using System.Collections.Generic;
using UnityEngine;

public class DestructionBlocks : MonoBehaviour
{
    [SerializeField] private FaceController _faceController;
    [SerializeField] private Player _player;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private GameObject _loadFacePanel;

    [Header("ViewDestructionBlocks")]
    [SerializeField] private ViewDestructionBlocks _viewDestructionBlocksHorizon;
    [SerializeField] private ViewDestructionBlocks _viewDestructionBlocksVertical;

    private bool _isWork = false;
    private Cell _targetCell;
    private List<Cell> _targetCells;
    private ExplosionConfigProvider _explosionProvider;
    private ExplosionObject _explosives;
    private RaycastHit _hit;
    private Ray _myRay;
   
    public bool IsWork => _isWork;

    private float _raycastDistance = 15;

    private void Awake()
    {
        _explosionProvider = new ExplosionConfigProvider(_faceController);
    }

    private void Update()
    {
        if(_isWork == false || _loadFacePanel.activeSelf)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_myRay, out _hit, _raycastDistance, _layerMask))
            {

                if (_hit.collider.TryGetComponent<ViewCell>(out ViewCell viewCell))
                {
                    if (_targetCell != viewCell.Cell)
                    {
                        ClearCell();

                        _targetCell = viewCell.Cell;
                        _targetCells = _explosionProvider.GetCellTarget(_targetCell, _explosives.Type);
                        Highlight();
                    }
                    else
                    {
                        if(_player.Coins >= _explosives.Price)
                        {
                            Detonate();
                            _player.�hange�oins(-_explosives.Price);
                        }

                        Clear();
                    }
                }

                UpdateView();
                _faceController.UpdateViewFaceController();
            }
        }
    }

    public void ButtonOnClick(ExplosionObject explosionObject)
    {
        if (_explosives == explosionObject)
        {
            Clear();
        }
        else
        {
            SetExplosives(explosionObject);
        }

        UpdateView();
        _faceController.UpdateViewFaceController();
    }

    public void Clear()
    {
        _isWork = false;
        _explosives = null;

        ClearCell();
        UpdateView();
    }

    private void SetExplosives(ExplosionObject explosionObject)
    {
        ClearCell();
        _isWork = true;
        _explosives = explosionObject;
    }

    private void UpdateView()
    {
        if(_explosives != null)
        {
            _viewDestructionBlocksHorizon.UpdateActiveImage(_explosives.Type);
            _viewDestructionBlocksVertical.UpdateActiveImage(_explosives.Type);
        }
        else
        {
            _viewDestructionBlocksHorizon.UpdateActiveImage(ExplosionType.Null);
            _viewDestructionBlocksVertical.UpdateActiveImage(ExplosionType.Null);
        }
    }

    private void ClearCell()
    {
        if (_targetCells != null)
        {
            foreach (var cell in _targetCells)
            {
                cell.SetTargetCell(false);
            }
        }

        _targetCell = null;
        _targetCells = null;
    }

    private void Highlight()
    {
        foreach (var cell in _targetCells)
        {
            cell.SetTargetCell(true);
        }
    }

    private void Detonate()
    {
        foreach(Cell cell  in _targetCells)
        {
            if(cell.Block != null)
            {
                cell.SetBoom(true);
            }
        }

        _audioSource.Play();
    }
}
