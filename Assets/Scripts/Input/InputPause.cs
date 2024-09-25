using System.Collections.Generic;
using UnityEngine;

public class InputPause : MonoBehaviour
{
    [SerializeField] private DestructionBlocks _destructionBlocks;
    [SerializeField] private BlockMover _blockMover;
    [SerializeField] private RotationCub _mouseRotationCub;
    [SerializeField] private AnimationFaceMove _animationFaceMove;
    [SerializeField] private List<GameObject> _panelBlockingInput = new List<GameObject>();

    private readonly float _delayInput = 0.01f;

    private float _currentDelayInput = 0;
    private bool _isCanMove = true;

    private void Update()
    {
        if(_currentDelayInput > 0)
        {
            _currentDelayInput -= Time.deltaTime;
        }

        if( _isCanMove != CheckIsCanInput())
        {
            if (_isCanMove)
            {
                _isCanMove = false;
            }
            else
            {
                _isCanMove = true;
                Delay();
            }
        }
    }

    public bool IsCanInput()
    {
        return _isCanMove && _currentDelayInput <= 0;
    }

    private void Delay()
    {
        _currentDelayInput = _delayInput;
    }

    private bool CheckIsCanInput()
    {
        foreach (GameObject panel in _panelBlockingInput)
        {
            if (panel.activeSelf == true)
            {
                return false;
            }
        }

        return _destructionBlocks.IsWork == false && _blockMover.IsMove == false 
            && _mouseRotationCub.IsWork == false && _animationFaceMove.IsMove == false;
    }
}
