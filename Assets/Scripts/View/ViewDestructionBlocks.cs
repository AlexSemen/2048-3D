using UnityEngine;
using UnityEngine.UI;

public class ViewDestructionBlocks : MonoBehaviour
{
    [SerializeField] private Player _player;

    [Header("Buttons")]
    [SerializeField] private Button _bulletButton;
    [SerializeField] private Button _bombButton;    
    [SerializeField] private Image _bulletButtonActivImage;
    [SerializeField] private Image _bombButtonActivImage;

    [Header("Explosion Objects")]
    [SerializeField] private ExplosionObject _bullet;
    [SerializeField] private ExplosionObject _bomb;


    private void OnEnable()
    {
        _player.ChangedCoins += CheckCanBuyExplosives;
        CheckCanBuyExplosives();
    }

    private void OnDisable()
    {
        _player.ChangedCoins -= CheckCanBuyExplosives;
    }

    public void UpdateActiveImage(ExplosionType explosionType)
    {
        _bulletButtonActivImage.gameObject.SetActive(explosionType == ExplosionType.Point);
        _bombButtonActivImage.gameObject.SetActive(explosionType == ExplosionType.Area);
    }

    private void CheckCanBuyExplosives()
    {
        _bulletButton.interactable = _player.Coins >= _bullet.Price;
        _bombButton.interactable = _player.Coins >= _bomb.Price;
    }
}
