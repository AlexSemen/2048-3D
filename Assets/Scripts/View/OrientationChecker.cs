using System;
using UnityEngine;

namespace View
{
    [ExecuteAlways]
    [DefaultExecutionOrder(-10)]
    public class OrientationChecker : MonoBehaviour
    {
        public event Action<bool> ChangedVertical;
        
        private bool _isVertical;

        public bool IsVertical => _isVertical;

        private void Awake()
        {
            HandleOrientation();
        }

        private void Start()
        {
            ChangedVertical?.Invoke(_isVertical);
        }

        void Update()
        {
            HandleOrientation();
        }

        private void HandleOrientation()
        {
            if (_isVertical && Screen.width > Screen.height * 1.4f)
            {
                _isVertical = false;
            }

            if (!_isVertical && Screen.width <= Screen.height * 1.4f)
            {
                _isVertical = true;
            }

            ChangedVertical?.Invoke(_isVertical);
        }
    }
}
