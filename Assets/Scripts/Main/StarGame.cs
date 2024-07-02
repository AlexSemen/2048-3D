using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarGame : MonoBehaviour
{
    [SerializeField] private ViewButtonMoveFace _viewButtonMove;
    [SerializeField] private FaceController _faceController;
    [SerializeField] private Player _player;
    [SerializeField] private LimitingMovements _limitingMovements;
    [SerializeField] private YandexLeaderboard _leaderboard;
    [SerializeField] private Button _rotationCubButtonHorizon1;
    [SerializeField] private Button _rotationCubButtonHorizon2;
    [SerializeField] private Button _rotationCubButtonVertical;

    public void StartGame(ShapeType shapeType, bool isLimitMove = false, int startPoints = 0, List<int> _blockValues = null)
    {
        if (shapeType == ShapeType.Classic)
            isLimitMove = false;

        _limitingMovements.Clear();

        _faceController.Init(shapeType, _blockValues);
        _player.SetPoints(startPoints);

        SetActivRotationCubButton(shapeType == ShapeType.Cub);
        _viewButtonMove.UpdateActiveButtons(shapeType);

        if (isLimitMove)
        {
            _limitingMovements.Init();
        }

        _leaderboard.SetSaveLeaderboard(shapeType, isLimitMove);

    }

    private void SetActivRotationCubButton(bool activ)
    {
        _rotationCubButtonHorizon1.gameObject.SetActive(activ);
        _rotationCubButtonHorizon2.gameObject.SetActive(activ);
        _rotationCubButtonVertical.gameObject.SetActive(activ);
    }
}
