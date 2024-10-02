using UnityEngine;

public class AnimationArrowMove : MonoBehaviour
{
    [SerializeField] private float _time = 0.5f;
    [SerializeField] private float _offsetX = 20f;
    [SerializeField] private float _offsetY = 20f;
    
    private Transform _transformMove;
    private float _currentTime;
    private Vector3 _defaultLocalPosition;

    private void Awake()
    {
        _transformMove = transform;
        _currentTime = _time;
        _defaultLocalPosition = _transformMove.localPosition;
    }

    private void OnDisable()
    {
        _transformMove.localPosition = _defaultLocalPosition;
        _currentTime = _time;
        _offsetX = System.Math.Abs(_offsetX);
    }

    private void Update()
    {
        if (_currentTime > 0) 
        {
            _currentTime -= Time.deltaTime;
        }
        else
        {
            _offsetX *= -1;
            _offsetY *= -1;
            _currentTime = _time;
        }

        _transformMove.position = new Vector3(_transformMove.position.x + _offsetX*Time.deltaTime, _transformMove.position.y 
            + _offsetY * Time.deltaTime, _transformMove.position.z);
    }
}
