using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewLimitingMovements : MonoBehaviour
{
    [SerializeField] private TMP_Text _currentCanMoveText;
    [SerializeField] private GameObject _limitingMovementsPanel;
    [SerializeField] private OrientationChecker _orientationChecker;
    [SerializeField] private Image _animationImage;

    private const float _timeAnimation = 0.2f;

    private Vector3 _limitPanelPositionHorizon = new Vector3(380, 380, 0);
    private Vector3 _limitPanelPositionVertical = new Vector3(100, 395, 0);
    private WaitForSecondsRealtime _waitForSecondsRealtime;
    private Coroutine _animationCoroutine;

    private void OnEnable()
    {
        _orientationChecker.ChangedVertical += UpdatePosition;
    }

    private void OnDisable()
    {
        _orientationChecker.ChangedVertical -= UpdatePosition;
    }

    private void Awake()
    {
        _waitForSecondsRealtime = new WaitForSecondsRealtime(_timeAnimation);
    }

    public void SetActivObject(bool activ)
    {
        _limitingMovementsPanel.SetActive(activ);
    }

    public void SetCurrentCanMoveText(string value)
    {
        _currentCanMoveText.text = value;
    }

    public void UpdatePosition(bool isVertical)
    {
        if (isVertical)
        {
            _limitingMovementsPanel.transform.localPosition = _limitPanelPositionVertical;
        }
        else
        {
            _limitingMovementsPanel.transform.localPosition = _limitPanelPositionHorizon;
        }
    }

    public void StartAnimation()
    {
        if(_animationCoroutine != null)
        {
            StopCoroutine(_animationCoroutine);
        }

        _animationCoroutine = StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        _animationImage.gameObject.SetActive(true);

        yield return _waitForSecondsRealtime;

        _animationImage.gameObject.SetActive(false);
    }
}
