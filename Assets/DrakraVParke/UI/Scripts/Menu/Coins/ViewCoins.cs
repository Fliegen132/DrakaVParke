using _2048Figure.Architecture.ServiceLocator;
using InstantGamesBridge;
using TMPro;
using UnityEngine;

public class ViewCoins : MonoBehaviour, IService
{
    [SerializeField] private TextMeshProUGUI _textCoins;
    public void Start()
    {
        _textCoins.text = $"{CoinsModel.GetCoinsCount()}";
        Bridge.advertisement.ShowBanner();
    }

    public void Buy(int price)
    {
        CoinsModel.SendCoin(price);
        _textCoins.text = $"{CoinsModel.GetCoinsCount()}";
    }

    public bool TryBuy(int price)
    {
        if (CoinsModel.GetCoinsCount() >= 300)
            return true;
        return false;
    }

    public void UpdateText()
    {
        _textCoins.text = $"{CoinsModel.GetCoinsCount()}";
    }
}
