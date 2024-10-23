using UnityEngine;
using DG.Tweening;
using View;
using Mover;

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

        public void MoveLine(MoveType moveType)
        {
            switch(moveType)
            {
                case MoveType.Left:
                    Play(0, _moveLineRotate);
                    break;
                    
                case MoveType.Right:
                    Play(0, -_moveLineRotate);
                    break;
            }
        }

        public void MoveCub(MoveType moveType)
        {
            switch (moveType)
            {
                case MoveType.Left:
                    Play(0, _moveCubRotate);
                    break;

                case MoveType.Right:
                    Play(0, -_moveCubRotate);
                    break;
                    
                case MoveType.Up:
                    Play(_moveCubRotate, 0);
                    break;
                    
                case MoveType.Down:
                    Play(-_moveCubRotate, 0);
                    break;
            }
        }

        private void Play(float x, float y)
        {
            _tween = _controller.transform.DORotate(new Vector3(x, y, 0), _time);
        }
    }
}
