using UnityEngine;

[RequireComponent(typeof(InputPause))]
public class InputPlayerMoveBlock : MonoBehaviour
{
    [SerializeField] private float _minDistanceSwipe = 400;
    [SerializeField] private LimitingMovements _limitingMovements;
    [SerializeField] private BlockMover _blockMover;

    private InputPause _inputPause;
    private Vector3 _mousePositionStart;
    private Vector3 _mousePositionEnd;
    private Vector3 _mousePositionMove;

    private void Awake()
    {
        _inputPause = GetComponent<InputPause>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePositionStart = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _mousePositionEnd = Input.mousePosition;

            if(Vector3.Distance(_mousePositionStart, _mousePositionEnd) > _minDistanceSwipe && IsCanMove())
            {
                SwipeDetected();
            }
        }

        if (Input.GetKeyUp(KeyCode.A) && IsCanMove())
        {
            _blockMover.Move(MoveType.Left);
        }

        if (Input.GetKeyUp(KeyCode.D) && IsCanMove())
        {
            _blockMover.Move(MoveType.Right);
        }

        if (Input.GetKeyUp(KeyCode.S) && IsCanMove())
        {
            _blockMover.Move(MoveType.Down);
        }

        if (Input.GetKeyUp(KeyCode.W) && IsCanMove())
        {
            _blockMover.Move(MoveType.Up);
        }
    }

    private bool IsCanMove()
    {
        return _inputPause.IsCanInput() && _limitingMovements.IsCanMoveBlockOnActiveFace();
    }

    private void SwipeDetected()
    {
        _mousePositionMove = _mousePositionEnd - _mousePositionStart;

        if (Mathf.Abs(_mousePositionMove.x) > Mathf.Abs(_mousePositionMove.y))
        {
            if(_mousePositionMove.x > 0)
            {
                _blockMover.Move(MoveType.Right);
            }
            else
            {
                _blockMover.Move(MoveType.Left);
            }
        }
        else
        {
            if(_mousePositionMove.y > 0)
            {
                _blockMover.Move(MoveType.Up);
            }
            else
            {
                _blockMover.Move(MoveType.Down);
            }
        }
    }
}
