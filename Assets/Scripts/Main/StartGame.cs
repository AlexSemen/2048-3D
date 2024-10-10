using Main.LimitingMover;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using View;
using View.UI;

namespace Main
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private ViewButtonMoveFace _viewButtonMoveFace;
        [SerializeField] private FaceController _faceController;
        [SerializeField] private Player _player;
        [SerializeField] private LimitingMovements _limitingMovements;
        [SerializeField] private YandexLeaderboard _yandexLeaderboard;
        [SerializeField] private Button _rotationCubeButtonHorizonLeft;
        [SerializeField] private Button _rotationCubeButtonHorizonRight;
        [SerializeField] private Button _rotationCubeButtonVertical;
        [SerializeField] private AutoPowerOff _helpHint;

        public void NewGame(ShapeType shapeType, bool isLimitMove = false, int startPoints = 0, List<int> _blockValues = null)
        {
            if (shapeType == ShapeType.Classic)
                isLimitMove = false;

            _limitingMovements.Clear();

            _faceController.Init(shapeType, _blockValues);
            _player.StartPoints(startPoints);

            SetActivRotationCubButton(shapeType == ShapeType.Cub);
            _viewButtonMoveFace.UpdateActiveButtons(shapeType);

            if (isLimitMove)
            {
                _limitingMovements.Init();
            }

            _yandexLeaderboard.SetSaveLeaderboard(shapeType, isLimitMove);

            if (_blockValues == null)
            {
                _helpHint.gameObject.SetActive(true);
            }
        }

        private void SetActivRotationCubButton(bool activ)
        {
            _rotationCubeButtonHorizonLeft.gameObject.SetActive(activ);
            _rotationCubeButtonHorizonRight.gameObject.SetActive(activ);
            _rotationCubeButtonVertical.gameObject.SetActive(activ);
        }
    }
}
