using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BlockMover : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private FaceController _faceController;
    [SerializeField] private ViewFaceController _viewFaceController;
    [SerializeField] private SpamerBlocks _spamerBlocks;
    [SerializeField] private SavingLoading _savingLoading;
    [SerializeField] private LimitingMovements _limitingMovements;

    private const int _maxBlockMeaning = 2048;
    private const float _delayMove = 0.051f;
    private const int _modifierBlockMeaning = 2;

    private AudioSource _audioSource;
    private WaitForSecondsRealtime _waitForSecondsRealtime;
    private bool _isSuccessfullyMoved = false;
    private Cell _cell;
    private Cell _targetCell;
    private bool _isMove = false;
    private bool _isWork = true;
    private bool _isAudio = false;

    public bool IsMove => _isMove;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _waitForSecondsRealtime = new WaitForSecondsRealtime(_delayMove);
    }

    public void Move(MoveType moveType)
    {
        if (_isMove)
            return;

        _isMove = true;

        switch (moveType)
        {
            case MoveType.Left:
                StartCoroutine(MovingLeft());
                break;
            
            case MoveType.Right:
                StartCoroutine(MovingRight());
                break;

            case MoveType.Up:
                StartCoroutine(MovingUp());
                break;

            case MoveType.Down:
                StartCoroutine(MovingDown());
                break;
        }
    }

    private IEnumerator MovingLeft()
    {
        do
        {
            _isWork = false;

            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(_faceController.ActiveFace.CellEdge - 1),
                new SettingsLoop(_faceController.ActiveFace.CellEdge - 1, 1)))
            {
                if (_faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J).Block != null)
                {
                    _cell = _faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J);
                    _targetCell = _faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J - 1);

                    if (TryMoving(_cell, _targetCell) || TryMerge(_cell, _targetCell))
                    {
                        SuccessfullyMoved(valueIJ.J, valueIJ.I, MoveType.Left);
                    }
                }
            }

            //for (int i = 0; i < _faceController.ActiveFace.CellEdge; i++)
            //{
            //    for (int j = 1; j < _faceController.ActiveFace.CellEdge; j++)
            //    {
            //        if (_faceController.ActiveFace.GetCell(i, j).Block != null)
            //        {
            //            _cell = _faceController.ActiveFace.GetCell(i, j);
            //            _targetCell = _faceController.ActiveFace.GetCell(i, j - 1);
            //
            //            if (TryMoving(_cell, _targetCell) || TryMerge(_cell, _targetCell))
            //            {
            //                SuccessfullyMoved(j, i, MoveType.Left);
            //            }
            //        }
            //    }
            //}

            yield return _waitForSecondsRealtime;

            _faceController.UpdateViewFaceController();
        } while (_isWork);

        FinishMoving();
    }

    private IEnumerator MovingRight()
    {
        do
        {
            _isWork = false;

            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(_faceController.ActiveFace.CellEdge - 1),
                new SettingsLoop(0, _faceController.ActiveFace.CellEdge - 2, false)))
            {
                if (_faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J).Block != null)
                {
                    _cell = _faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J);
                    _targetCell = _faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J + 1);

                    if (TryMoving(_cell, _targetCell) || TryMerge(_cell, _targetCell))
                    {
                        SuccessfullyMoved(valueIJ.I, valueIJ.J, MoveType.Right);
                    }
                }
            }

            //for (int i = 0; i < _faceController.ActiveFace.CellEdge; i++)
            //{
            //    for (int j = _faceController.ActiveFace.CellEdge - 2; j >= 0; j--)
            //    {
            //        if (_faceController.ActiveFace.GetCell(i, j).Block != null)
            //        {
            //            _cell = _faceController.ActiveFace.GetCell(i, j);
            //            _targetCell = _faceController.ActiveFace.GetCell(i, j + 1);
            //
            //            if (TryMoving(_cell, _targetCell) || TryMerge(_cell, _targetCell))
            //            {
            //                SuccessfullyMoved(j, i, MoveType.Right);
            //            }
            //        }
            //    }
            //}

            yield return _waitForSecondsRealtime;

            _faceController.UpdateViewFaceController();
        } while (_isWork);

        FinishMoving();
    }

    private IEnumerator MovingUp()
    {
        do
        {
            _isWork = false;

            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(_faceController.ActiveFace.CellEdge - 1),
                new SettingsLoop(_faceController.ActiveFace.CellEdge - 1, 1)))
            {
                if (_faceController.ActiveFace.GetCell(valueIJ.J, valueIJ.I).Block != null)
                {
                    _cell = _faceController.ActiveFace.GetCell(valueIJ.J, valueIJ.I);
                    _targetCell = _faceController.ActiveFace.GetCell(valueIJ.J - 1, valueIJ.I);

                    if (TryMoving(_cell, _targetCell) || TryMerge(_cell, _targetCell))
                    {
                        SuccessfullyMoved(valueIJ.I, valueIJ.J, MoveType.Up);
                    }
                }
            }

            //for (int i = 0; i < _faceController.ActiveFace.CellEdge; i++)
            //{
            //    for (int j = 1; j < _faceController.ActiveFace.CellEdge; j++)
            //    {
            //        if (_faceController.ActiveFace.GetCell(j, i).Block != null)
            //        {
            //            _cell = _faceController.ActiveFace.GetCell(j, i);
            //            _targetCell = _faceController.ActiveFace.GetCell(j - 1, i);
            //
            //            if (TryMoving(_cell, _targetCell) || TryMerge(_cell, _targetCell))
            //            {
            //                SuccessfullyMoved(i, j, MoveType.Up);
            //            }
            //        }
            //    }
            //}

            yield return _waitForSecondsRealtime;

            _faceController.UpdateViewFaceController();
        } while (_isWork);

        FinishMoving();
    }

    private IEnumerator MovingDown()
    {
        do
        {
            _isWork = false;

            foreach (ValueIJ valueIJ in DoubleLoop.GetValues(new SettingsLoop(_faceController.ActiveFace.CellEdge - 1),
               new SettingsLoop(0, _faceController.ActiveFace.CellEdge - 2, false)))
            {
                if (_faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J).Block != null)
                {
                    _cell = _faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J);
                    _targetCell = _faceController.ActiveFace.GetCell(valueIJ.I + 1, valueIJ.J);

                    if (TryMoving(_cell, _targetCell) || TryMerge(_cell, _targetCell))
                    {
                        SuccessfullyMoved(valueIJ.J, valueIJ.I, MoveType.Down);
                    }
                }
            }

            //for (int j = 0; j < _faceController.ActiveFace.CellEdge; j++)
            //{
            //    for (int i = _faceController.ActiveFace.CellEdge - 2; i >= 0; i--)
            //    {
            //        if (_faceController.ActiveFace.GetCell(i, j).Block != null)
            //        {
            //            _cell = _faceController.ActiveFace.GetCell(i, j);
            //            _targetCell = _faceController.ActiveFace.GetCell(i + 1, j);
            //
            //            if (TryMoving(_cell, _targetCell) || TryMerge(_cell, _targetCell))
            //            {
            //                SuccessfullyMoved(j, i, MoveType.Down);
            //            }
            //        }
            //    }
            //}
        
            yield return _waitForSecondsRealtime;

            _faceController.UpdateViewFaceController();
        } while (_isWork) ;

         FinishMoving();
    }

    private void SuccessfullyMoved(int i, int j, MoveType moveType)
    {
        _viewFaceController.ActiveViewFace.MoveBlock(j, i, moveType);
        _isWork = true;
        _isSuccessfullyMoved = true;

        if (_isAudio == false)
        {
            _isAudio = true;
            _audioSource.Play();
        }
    }

    private bool TryMoving(Cell cell, Cell targetCell)
    {
        if (targetCell.Block == null)
        {
            targetCell.SetBlock(cell.Block);
            cell.SetBlock(null);
            
            return true;
        }

        return false;
    }

    private bool TryMerge(Cell cell, Cell targetCell)
    {
        if (targetCell.Block.Meaning == cell.Block.Meaning && cell.Block.IsCanCombined && targetCell.Block.IsCanCombined)
        {
            _player.AddPoints((CellType)targetCell.Block.Meaning);

            targetCell.Block.SetMeaning(targetCell.Block.Meaning * _modifierBlockMeaning);
            cell.SetBlock(null);

            if (targetCell.Block.Meaning > _maxBlockMeaning)
            {
                targetCell.SetBlock(null);
            }

            return true;
        }

        return false;
    }

    private void FinishMoving()
    {
        if (_isSuccessfullyMoved)
        {
            _faceController.ActiveFace.ThrowStatusBlock();
            _spamerBlocks.TrySpamBlok(_faceController.ActiveFace);
            _spamerBlocks.TrySpamBlokRandomFace();
            _limitingMovements.Move();
            _faceController.UpdateViewFaceController();
            _savingLoading.CreateSaveRequest();
            _isSuccessfullyMoved = false;
        }

        _isMove = false;
        _isAudio = false;
    }
}
