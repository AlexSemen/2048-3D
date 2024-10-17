using Main;
using UnityEngine;
using View.UI;

namespace View
{
    public class ViewButtonMoveFace : MonoBehaviour
    {
        [SerializeField] private PanelObject _moveCubButtonsHorizon;
        [SerializeField] private PanelObject _moveLineButtonsHorizon;
        [SerializeField] private PanelObject _moveCubButtonsVertical;
        [SerializeField] private PanelObject _moveLineButtonsVertical;
        [SerializeField] private ViewVerticalButtons _viewVerticalButtons;

        public void UpdateActiveButtons(ShapeType shapeType)
        {
            if (shapeType == ShapeType.Classic)
            {
                _moveCubButtonsVertical.gameObject.SetActive(false);
                _moveLineButtonsVertical.gameObject.SetActive(false);
                _moveCubButtonsHorizon.gameObject.SetActive(false);
                _moveLineButtonsHorizon.gameObject.SetActive(false);
            }
            else
            {
                _moveLineButtonsHorizon.gameObject.SetActive(true);
                _moveCubButtonsHorizon.gameObject.SetActive(shapeType == ShapeType.Cub);
                _moveCubButtonsVertical.gameObject.SetActive(shapeType == ShapeType.Cub);
                _moveLineButtonsVertical.gameObject.SetActive(shapeType != ShapeType.Cub);
            }

            _viewVerticalButtons.UpdateView();
        }
    }
}
