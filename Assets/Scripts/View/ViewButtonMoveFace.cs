using UnityEngine;

public class ViewButtonMoveFace : MonoBehaviour
{
    [SerializeField] private GameObject _moveCubButtonsHorizon;
    [SerializeField] private GameObject _moveLineButtonsHorizon;
    [SerializeField] private GameObject _moveCubButtonsVertical;
    [SerializeField] private GameObject _moveLineButtonsVertical;
    [SerializeField] private ViewVerticalButtons _viewVerticalButtons;

    public void UpdateActiveButtons(ShapeType shapeType)
    {
        if(shapeType == ShapeType.Classic)
        {
            _moveCubButtonsVertical.SetActive(false);
            _moveLineButtonsVertical.SetActive(false);
            _moveCubButtonsHorizon.gameObject.SetActive(false);
            _moveLineButtonsHorizon.gameObject.SetActive(false);
            _viewVerticalButtons.UpdateView();

        }
        else
        {
            _moveLineButtonsHorizon.gameObject.SetActive(true);
            _moveCubButtonsHorizon.gameObject.SetActive(shapeType == ShapeType.Cub);
            _moveCubButtonsVertical.SetActive(shapeType == ShapeType.Cub);
            _moveLineButtonsVertical.SetActive(shapeType != ShapeType.Cub);
            _viewVerticalButtons.UpdateView();
        }
    }
}
