using UnityEngine;
using DG.Tweening;

public class AnimationFaceMove : MonoBehaviour
{
    [SerializeField] private ViewFaceController _controller;

    private readonly float _time = 0.5f;
    private readonly float MoveLineRotate = 20.5f;
    private readonly float MoveCubRotate = 90;

    private bool _isTween;
    private Tween _tween;

    public bool IsMove => _isTween;

    private void Update()
    {
        if (_isTween == false && _tween != null && _tween.active)
        {
            _isTween = true;
        }

        if (_isTween && _tween.active == false)
        {
            _controller.transform.rotation = Quaternion.identity;
            _controller.UpdateViewFaces();
            _isTween = false;
        }
    }

    public void MoveLeftLine()
    {
        _tween = _controller.transform.DORotate(new Vector3(0, MoveLineRotate, 0), _time);
    }

    public void MoveRightLine()
    {
        _tween = _controller.transform.DORotate(new Vector3(0, -MoveLineRotate, 0), _time);
    }

    public void MoveLeftCub()
    {
        _tween = _controller.transform.DORotate(new Vector3(0, MoveCubRotate, 0), _time);
    }

    public void MoveRightCub()
    {
        _tween = _controller.transform.DORotate(new Vector3(0, -MoveCubRotate, 0), _time);
    }

    public void MoveUpCub()
    {
        _tween = _controller.transform.DORotate(new Vector3(MoveCubRotate, 0, 0), _time);
    }

    public void MoveDownCub()
    {
        _tween = _controller.transform.DORotate(new Vector3(-MoveCubRotate, 0, 0), _time);
    }
}
