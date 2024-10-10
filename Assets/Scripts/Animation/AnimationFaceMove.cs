using UnityEngine;
using DG.Tweening;
using View;

namespace Animation
{
    public class AnimationFaceMove : MonoBehaviour
    {
        private readonly float _time = 0.5f;
        private readonly float _moveLineRotate = 20.5f;
        private readonly float _moveCubRotate = 90;
        
        [SerializeField] private ViewFaceController _controller;

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
            Play(0, _moveLineRotate);
        }

        public void MoveRightLine()
        {
            Play(0, -_moveLineRotate);
        }

        public void MoveLeftCub()
        {
            Play(0, _moveCubRotate);
        }

        public void MoveRightCub()
        {
            Play(0, -_moveCubRotate);
        }

        public void MoveUpCub()
        {
            Play(_moveCubRotate, 0);
        }

        public void MoveDownCub()
        {
            Play(-_moveCubRotate, 0);
        }

        private void Play(float x, float y)
        {
            _tween = _controller.transform.DORotate(new Vector3(x, y, 0), _time);
        }
    }
}
