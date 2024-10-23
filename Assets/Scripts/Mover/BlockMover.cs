using System.Collections;
using UnityEngine;
using Data.CellData;
using Loop;
using Main;
using Main.LimitingMover;
using View;
using Yandex.SaveLoad;
using System;
using System.Collections.Generic;

namespace Mover 
{
    [RequireComponent(typeof(AudioSource))]
    public class BlockMover : MonoBehaviour
    {
        private const int _maxBlockMeaning = 2048;
        private const float _delayMove = 0.051f;
        private const int _modifierBlockMeaning = 2;
        private const int _cellEdge = 4;

        [SerializeField] private Player _player;
        [SerializeField] private FaceController _faceController;
        [SerializeField] private ViewFaceController _viewFaceController;
        [SerializeField] private SpamerBlocks _spamerBlocks;
        [SerializeField] private SavingLoading _savingLoading;
        [SerializeField] private LimitingMovements _limitingMovements;

        private readonly SettingsLoop _settingsLoopDefaultIncreasing = new SettingsLoop(_cellEdge - 1);
        private readonly SettingsLoop _settingsLoopDefaultIncreasingStartingFromValueOne = new SettingsLoop(_cellEdge - 1, 1);
        private readonly SettingsLoop _settingsLoopDefaultDecreasing = new SettingsLoop(0, _cellEdge - 2, false);

        private AudioSource _audioSource;
        private WaitForSecondsRealtime _waitForSecondsRealtime;
        private bool _isSuccessfullyMoved = false;
        private Cell _cell;
        private Cell _targetCell;
        private bool _isMove = false;
        private bool _isWork = true;
        private bool _isAudio = false;
        private List<ValueIJ> _valueIJs;
        private MoveType _currentMoveType;

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

            StartCoroutine(Moving(moveType));
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

        private IEnumerator Moving(MoveType moveType)
        {
            _valueIJs = GetValueIJ(moveType);
            _currentMoveType = moveType;

            do
            {
                _isWork = false;

                switch (moveType)
                {
                    case MoveType.Left:
                        MoveCells((ValueIJ valueIJ) => new Vector2Int(valueIJ.I, valueIJ.J - 1));
                        break;
                    case MoveType.Right:
                        MoveCells((ValueIJ valueIJ) => new Vector2Int(valueIJ.I, valueIJ.J + 1));
                        break;
                    case MoveType.Up:
                        MoveCells((ValueIJ valueIJ) => new Vector2Int(valueIJ.I - 1, valueIJ.J));
                        break;
                    case MoveType.Down:
                        MoveCells((ValueIJ valueIJ) => new Vector2Int(valueIJ.I + 1, valueIJ.J));
                        break;
                }

                yield return _waitForSecondsRealtime;

                _faceController.UpdateViewFaceController();
            } while (_isWork);

            FinishMoving();
        }

        private List<ValueIJ> GetValueIJ(MoveType moveType)
        {
            switch (moveType)
            {
                case MoveType.Left:
                    return DoubleLoop.GetValues(_settingsLoopDefaultIncreasing, _settingsLoopDefaultIncreasingStartingFromValueOne);

                case MoveType.Right:
                    return DoubleLoop.GetValues(_settingsLoopDefaultIncreasing, _settingsLoopDefaultDecreasing);
                    
                case MoveType.Up:
                    return DoubleLoop.GetValues(_settingsLoopDefaultIncreasing, _settingsLoopDefaultIncreasingStartingFromValueOne, true);

                case MoveType.Down:
                    return DoubleLoop.GetValues(_settingsLoopDefaultDecreasing, _settingsLoopDefaultIncreasing);

                default:
                    throw new ArgumentOutOfRangeException();

            }
        }

        private void MoveCells(Func<ValueIJ, Vector2Int> targetCell—oordinates)
        {
            foreach (ValueIJ valueIJ in _valueIJs)
            {
                if (_faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J).Block != null)
                {
                    _cell = _faceController.ActiveFace.GetCell(valueIJ.I, valueIJ.J);

                    _targetCell = _faceController.ActiveFace.GetCell(targetCell—oordinates(valueIJ));

                    if (TryMoving(_cell, _targetCell) || TryMerge(_cell, _targetCell))
                    {
                        SuccessfullyMoved(valueIJ.J, valueIJ.I, _currentMoveType);
                    }
                }
            }
        }
    }
}
