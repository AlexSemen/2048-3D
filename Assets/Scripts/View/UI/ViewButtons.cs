using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewButtons : MonoBehaviour
{
    [SerializeField] private Button _classicButton;
    [SerializeField] private Button _lineButton;
    [SerializeField] private Button _cubButton;
    [SerializeField] private Button _limitButton;
    [SerializeField] private ButtonViewDatasList _buttonlViewDatasList;
    [SerializeField] private Button _playButton;
    [SerializeField] private Image _continueImage;

    private readonly Dictionary<ButtonType, ButtonViewData> _buttonByCellType = new Dictionary<ButtonType, ButtonViewData>();

    public void Init()
    {
        foreach (ButtonViewData buttonViewData in _buttonlViewDatasList.List)
        {
            _buttonByCellType.Add(buttonViewData.Type, buttonViewData);
        }
    }

    public void Clear()
    {
        _classicButton.image.sprite = GetButtonViewData(ButtonType.Classic).Sprite1;
        _lineButton.image.sprite = GetButtonViewData(ButtonType.Line).Sprite1;
        _cubButton.image.sprite = GetButtonViewData(ButtonType.Cub).Sprite1;
        _limitButton.image.sprite = GetButtonViewData(ButtonType.Limit).Sprite1;
    }

    public void UpdateButtons(ShapeType shapeType, bool isLimit, bool isPlay = false, bool isContinue = false)
    {
        Clear();

        switch (shapeType)
        {
            case ShapeType.Classic:
                _classicButton.image.sprite = GetButtonViewData(ButtonType.Classic).Sprite2;
                break; 

            case ShapeType.Line:
                _lineButton.image.sprite = GetButtonViewData(ButtonType.Line).Sprite2;
                break;

            case ShapeType.Cub:
                _cubButton.image.sprite = GetButtonViewData(ButtonType.Cub).Sprite2;
                break;
        }

        if(isLimit)
        {
            _limitButton.image.sprite = GetButtonViewData(ButtonType.Limit).Sprite2;
        }

        if (_playButton != null)
        {
            if (isPlay == false && isContinue)
            {
                _playButton.image.sprite = GetButtonViewData(ButtonType.Play).Sprite2;
            }
            else
            {
                _playButton.image.sprite = GetButtonViewData(ButtonType.Play).Sprite1;
            }

            _playButton.interactable = !(isPlay == false && isContinue == false);

            _continueImage.gameObject.SetActive(isPlay && isContinue);
        }
    }

    public ButtonViewData GetButtonViewData(ButtonType buttonType)
    {
        if (_buttonByCellType.ContainsKey(buttonType))
            return _buttonByCellType[buttonType];

        Debug.Log(buttonType.ToString());
        throw new Exception($"Sprite for cellType {buttonType} does not exist!");
    }
}
