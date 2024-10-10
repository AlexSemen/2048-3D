using UnityEngine;
using DG.Tweening;
using Mover;

namespace Animation
{
    public class AnimationBlockMove : MonoBehaviour
    {
        private readonly float _time = 0.05f;
        private readonly float _offset = 1;

        private float _offsetX;
        private float _offsetY;

        public void Move(Transform targetTransform, MoveType moveType)
        {
            _offsetX = 0;
            _offsetY = 0;

            switch (moveType)
            {
                case MoveType.Left:
                    _offsetX = -_offset;
                    break;

                case MoveType.Right:
                    _offsetX = _offset;
                    break;

                case MoveType.Up:
                    _offsetY = _offset;
                    break;

                case MoveType.Down:
                    _offsetY = -_offset;
                    break;
            }

            targetTransform.DOLocalMove(new Vector3(_offsetX, _offsetY, targetTransform.localPosition.z), _time);
        }
    }
}
