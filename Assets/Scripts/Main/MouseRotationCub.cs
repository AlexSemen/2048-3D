using UnityEngine;

public class MouseRotationCub : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ViewCamera _viewCamera;
    [SerializeField] private FaceController _faceController;
    [SerializeField] private LimitingMovements _limitingMovements;
    [SerializeField] private DestructionBlocks _destructionBlocks;

    private const float  sensitivity = 1.5f;
    private const float _maxTimeOneClick = 0.5f;
    private const float _raycastDistance = 20;

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
            float rotationX = Input.GetAxis("Mouse X") * sensitivity;
            float rotationY = Input.GetAxis("Mouse Y") * sensitivity;

            transform.Rotate(Vector3.up, -rotationX, Space.World);
            transform.Rotate(Vector3.right, rotationY, Space.World);

            _timeDownClick += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(_timeDownClick <= _maxTimeOneClick)
            {
                _myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_myRay, out _hit, _raycastDistance, _layerMask))
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
    }

    private void TurnOff()
    {
        _isWork = false;
        transform.rotation = Quaternion.identity;
        _viewCamera.SetMouseRotationCub(false);
        _limitingMovements.TurnOnView();
    }
}
