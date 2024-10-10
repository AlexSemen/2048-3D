using Main;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View.UI.ButtonsData;

namespace View.UI
{
    public class ViewButtons : MonoBehaviour
    {
        [SerializeField] private Button _classicButton;
        [SerializeField] private Button _lineButton;
        [SerializeField] private Button _cubButton;
        [SerializeField] private Button _limitButton;
        [SerializeField] private ButtonViewDataList _buttonlViewDatasList;
        [SerializeField] private Button _playButton;
        [SerializeField] private Image _continueImage;

        private int _indexSpriteDefault = 0;
        private int _indexSpriteActive = 1;

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
            _classicButton.image.sprite = GetButtonViewData(ButtonType.Classic).Sprites[_indexSpriteDefault];
            _lineButton.image.sprite = GetButtonViewData(ButtonType.Line).Sprites[_indexSpriteDefault];
            _cubButton.image.sprite = GetButtonViewData(ButtonType.Cub).Sprites[_indexSpriteDefault];
            _limitButton.image.sprite = GetButtonViewData(ButtonType.Limit).Sprites[_indexSpriteDefault];
        }

        public void UpdateButtons(ShapeType shapeType, bool isLimit, bool isPlay = false, bool isContinue = false)
        {
            Clear();

            switch (shapeType)
            {
                case ShapeType.Classic:
                    _classicButton.image.sprite = GetButtonViewData(ButtonType.Classic).Sprites[_indexSpriteActive];
                    break;

                case ShapeType.Line:
                    _lineButton.image.sprite = GetButtonViewData(ButtonType.Line).Sprites[_indexSpriteActive];
                    break;

                case ShapeType.Cub:
                    _cubButton.image.sprite = GetButtonViewData(ButtonType.Cub).Sprites[_indexSpriteActive];
                    break;
            }

            if (isLimit)
            {
                _limitButton.image.sprite = GetButtonViewData(ButtonType.Limit).Sprites[_indexSpriteActive];
            }

            if (_playButton != null)
            {
                if (isPlay == false && isContinue)
                {
                    _playButton.image.sprite = GetButtonViewData(ButtonType.Play).Sprites[_indexSpriteActive];
                }
                else
                {
                    _playButton.image.sprite = GetButtonViewData(ButtonType.Play).Sprites[_indexSpriteDefault];
                }

                _playButton.interactable = !(isPlay == false && isContinue == false);

                _continueImage.gameObject.SetActive(isPlay && isContinue);
            }
        }

        public ButtonViewData GetButtonViewData(ButtonType buttonType)
        {
            if (_buttonByCellType.ContainsKey(buttonType))
                return _buttonByCellType[buttonType];

            throw new Exception($"Sprite for cellType {buttonType} does not exist!");
        }
    }
}
