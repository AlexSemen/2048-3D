using UnityEngine;

public class StartMenuPanel : MonoBehaviour
{
    [SerializeField] private StarGame _starGame;
    [SerializeField] private GameObject _newGamePanel;
    [SerializeField] private FaceController _faceController;
    [SerializeField] private ViewVerticalButtons _viewVerticalButtons;
    [SerializeField] private ViewButtons _viewStartMenuPanelButtonsHorizon;
    [SerializeField] private ViewButtons _viewStartMenuPanelButtonsVertical;
    
    private ShapeType _shapeType = ShapeType.Null;
    private bool _isLimit = false;

    private void Awake()
    {
        _viewStartMenuPanelButtonsHorizon.Init();
        _viewStartMenuPanelButtonsVertical.Init(); 
    }

    private void OnEnable()
    {
        UpdateViewButtons();
    }

    private void OnDisable()
    {
        Clear();
        _viewVerticalButtons.UpdateView();
    }

    public void OnClickClassic()
    {
        SetShapType(ShapeType.Classic);
    }

    public void OnClickLine()
    {
        SetShapType(ShapeType.Line);
    }

    public void OnClickCub()
    {
        SetShapType(ShapeType.Cub);
    }

    public void OnClickLimit()
    {
        _isLimit = !_isLimit;

        UpdateViewButtons();
    }

    public void OnClickPlay()
    {
        if(_shapeType == ShapeType.Null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            if (_faceController.Faces != null && _faceController.Faces.Count > 0)
            {
                _newGamePanel.SetActive(true);
            }
            else
            {
                StartNewGame();
                gameObject.SetActive(false);
            }
        }
    }

    public void StartNewGame()
    {
        _starGame.StartGame(_shapeType, _isLimit);
        gameObject.SetActive(false);
    }

    public void UpdateViewButtons()
    {
        _viewStartMenuPanelButtonsHorizon.UpdateButtons(_shapeType, _isLimit, _shapeType != ShapeType.Null, _faceController.Faces != null);
        _viewStartMenuPanelButtonsVertical.UpdateButtons(_shapeType, _isLimit, _shapeType != ShapeType.Null, _faceController.Faces != null);
    }

    private void SetShapType(ShapeType shapeType)
    {
        if(_shapeType != shapeType)
        {
            _shapeType = shapeType;
        }
        else
        {
            _shapeType = ShapeType.Null;
        }

        UpdateViewButtons();
    }

    private void Clear()
    {
        _shapeType = ShapeType.Null;
        _isLimit = false;
    }
}
