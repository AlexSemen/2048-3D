using Explosives;
using Main.LimitingMover;
using UnityEngine;
using View;
using View.UI;

namespace Main
{
    public class RotationCub : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private ViewCamera _viewCamera;
        [SerializeField] private FaceController _faceController;
        [SerializeField] private LimitingMovements _limitingMovements;
        [SerializeField] private DestructionBlocks _destructionBlocks;
        [SerializeField] private Orientation _noRotationCubButtons;
        [SerializeField] private Orientation _rotationCubHalp;

        private const float Sensitivity = 1.5f;
        private const float MaxTimeOneClick = 0.5f;
        private const float RaycastDistance = 20;

        private float _timeDownClick;
        private RaycastHit _hit;
        private Ray _myRay;
        private bool _isWork;

        public bool IsWork => _isWork;

        private void Update()
        {
            if (_isWork == false)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _timeDownClick = 0;
            }

            if (Input.GetMouseButton(0))
            {
                float rotationX = Input.GetAxis("Mouse X") * Sensitivity;
                float rotationY = Input.GetAxis("Mouse Y") * Sensitivity;

                transform.Rotate(Vector3.up, -rotationX, Space.World);
                transform.Rotate(Vector3.right, rotationY, Space.World);

                _timeDownClick += Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_timeDownClick <= MaxTimeOneClick)
                {
                    _myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(_myRay, out _hit, RaycastDistance, _layerMask))
                    {

                        if (_hit.collider.TryGetComponent<ViewFace>(out ViewFace viewFace))
                        {
                            _faceController.SetActivFace(viewFace.Face);
                            TurnOff();
                        }
                    }
                }
            }
        }

        public void TurnOn()
        {
            _isWork = true;
            _viewCamera.SetMouseRotationCub(true);
            _destructionBlocks.Clear();
            _faceController.UpdateViewFaceController();
            _limitingMovements.TurnOffView();
            _noRotationCubButtons.gameObject.SetActive(true);
            _rotationCubHalp.gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            _isWork = false;
            transform.rotation = Quaternion.identity;
            _viewCamera.SetMouseRotationCub(false);
            _limitingMovements.TurnOnView();
            _noRotationCubButtons.gameObject.SetActive(false);
            _rotationCubHalp.gameObject.SetActive(false);
        }
    }
}
