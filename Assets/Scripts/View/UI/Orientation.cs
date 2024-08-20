using UnityEngine;

public class Orientation : MonoBehaviour
{
    [SerializeField] private GameObject _vertical;
    [SerializeField] private GameObject _horizon;
    [SerializeField] private OrientationChecker _orientationChecker;

    private void OnEnable()
    {
        SetValue(_orientationChecker.IsVertical);
        _orientationChecker.ChangedVertical += SetValue;
    }

    private void OnDisable()
    {
        _orientationChecker.ChangedVertical -= SetValue;
    }

    private void SetValue(bool vertical)
    {
        _vertical.gameObject.SetActive(vertical);
        _horizon.gameObject.SetActive(!vertical);
    }
}
