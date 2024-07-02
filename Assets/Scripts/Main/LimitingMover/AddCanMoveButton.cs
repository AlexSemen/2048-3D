using UnityEngine;
using UnityEngine.UI;

public class AddCanMoveButton : MonoBehaviour
{
    [SerializeField] private LimitingMovements _limitingMovements;
    [SerializeField] private Player _player;
    [SerializeField] private int _price;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _player.ChangedCoins += CheckCanBuy;
    }

    private void OnDisable()
    {
        _player.ChangedCoins -= CheckCanBuy;
    }

    public void AddCanMoveOnClick()
    {
        if (_player.Coins >= _price)
        {
            _limitingMovements.AddCanMoveActivFace();
            _player.ÑhangeÑoins(-_price);
        }
    }

    private void CheckCanBuy()
    {
        _button.interactable = _player.Coins >= _price;
    }
}
