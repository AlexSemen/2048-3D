using UnityEngine;
using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine.UI;

public class InAdd : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private AddCoinsDatasList _inAddDatasList;
    [SerializeField] private Button _noStickyButton;

    private const string StringNoSticky = "NoSticky";

    private bool _isBuyNoSticky = false;

    public bool IsBuyNoSticky => _isBuyNoSticky;

    private Dictionary<AddCoinsType, AddCoinsData> _inAddDataByInAddType;

    private void Awake()
    {
        _inAddDataByInAddType = new Dictionary<AddCoinsType, AddCoinsData>();

        foreach (AddCoinsData inAddData in _inAddDatasList.InAddDatas)
        {
            _inAddDataByInAddType.Add(inAddData.Type, inAddData);
        }
    }

    public void OnClickButtonAddCoins(int coins)
    {
        switch (coins)
        {
            case 1:
                BuyCoins(AddCoinsType.Coins1);
                break; 

            case 5:
                BuyCoins(AddCoinsType.Coins5);
                break;
                
            case 10:
                BuyCoins(AddCoinsType.Coins10);
                break; 

            case 50:
                BuyCoins(AddCoinsType.Coins50);
                break; 

            case 100:
                BuyCoins(AddCoinsType.Coins100);
                break;
        }
    }

    public void NoSticky()
    {
#if !UNITY_EDITOR
        Billing.PurchaseProduct(StringNoSticky, (purchaseProductResponse) =>
        {
            Billing.ConsumeProduct(purchaseProductResponse.purchaseData.purchaseToken, () =>
            {
#endif
                SetIsBuyNoSticky(true);
#if !UNITY_EDITOR
            });
        });
#endif
    }

    private void BuyCoins(AddCoinsType inAddType)
    {
#if !UNITY_EDITOR
        Billing.PurchaseProduct(_inAddDataByInAddType[inAddType].Name, (purchaseProductResponse) =>
        {
            Billing.ConsumeProduct(purchaseProductResponse.purchaseData.purchaseToken, () =>
            {
#endif
                  _player.—hange—oins(_inAddDataByInAddType[inAddType].AddCoins);
#if !UNITY_EDITOR
            });
        });
#endif
    }

    public void SetIsBuyNoSticky(bool isBuyNoSticky)
    {
        if (isBuyNoSticky)
        {
            _isBuyNoSticky = true;
            _noStickyButton.interactable = false;
#if !UNITY_EDITOR
            StickyAd.Hide();
#endif
        }
        else
        {
            _isBuyNoSticky = false;
            _noStickyButton.interactable = true;
#if !UNITY_EDITOR
            StickyAd.Show();
#endif
        }
    }
}
