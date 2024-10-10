using UnityEngine;

namespace View.UI
{
    [RequireComponent(typeof(OrientationChecker))]
    public class ViewCamera : MonoBehaviour
    {
        private const float CameraHorizonY = 0;
        private const float CameraHorizonZ = -6;
        private const float CameraVerticalY = -0.4f;
        private const float CameraVerticaNoRotationCubZ = -7;
        private const float CameraVerticalRotationCubZ = -9f;
        private const float BackgroundRotationCubZ = 4;

        [SerializeField] private GameObject _verticalPanel;
        [SerializeField] private GameObject _horizonPanel;
        [SerializeField] Transform _background;
        [SerializeField] private ViewDestructionBlocks _destructionBlocksHorizon;

        private Transform _cameraTransform;
        private OrientationChecker _orientationChecker;

        private float _backgroundNoRotationCubZ;
        private bool _isVertical = false;
        private bool _isMouseRotationCub = false;
        private float y;
        private float z;

        private void Awake()
        {
            _orientationChecker = GetComponent<OrientationChecker>();

            _backgroundNoRotationCubZ = _background.transform.localScale.z;
            _cameraTransform = transform;
        }

        private void OnEnable()
        {
            _orientationChecker.ChangedVertical += SetIsVertical;
        }

        private void OnDisable()
        {
            _orientationChecker.ChangedVertical -= SetIsVertical;
        }

        public void UpdateView()
        {
            if (_isVertical)
            {
                y = CameraVerticalY;

                if (_isMouseRotationCub)
                {
                    z = CameraVerticalRotationCubZ;
                }
                else
                {
                    z = CameraVerticaNoRotationCubZ;
                }
            }
            else
            {
                y = CameraHorizonY;
                z = CameraHorizonZ;
            }

            if (_isMouseRotationCub == false)
            {
                _verticalPanel.SetActive(_isVertical);
                _horizonPanel.SetActive(!_isVertical);
                _destructionBlocksHorizon.gameObject.SetActive(!_isVertical);

                _background.localPosition = new Vector3(0, 0, _backgroundNoRotationCubZ);
            }
            else
            {
                _verticalPanel.SetActive(false);
                _horizonPanel.SetActive(false);
                _destructionBlocksHorizon.gameObject.SetActive(false);

                _background.localPosition = new Vector3(0, 0, BackgroundRotationCubZ);
            }

            _cameraTransform.position = new Vector3(0, y, z);
        }

        public void SetIsVertical(bool value)
        {
            _isVertical = value;
            UpdateView();
        }

        public void SetMouseRotationCub(bool value)
        {
            _isMouseRotationCub = value;
            UpdateView();
        }
    }
}
