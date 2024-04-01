using _2048Figure.Architecture.ServiceLocator;
using TMPro;
using UnityEngine;

public class ViewCoins : MonoBehaviour, IService
{
    [SerializeField] private TextMeshProUGUI _textCoins;
    public void Start()
    {
        _textCoins.text = $"{CoinsModel.GetCoinsCount()}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CoinsModel.AddCoins(300);
            _textCoins.text = $"{CoinsModel.GetCoinsCount()}";
        }
    }

    public bool Buy(int price)
    {
        bool can = CoinsModel.SendCoin(price);
        _textCoins.text = $"{CoinsModel.GetCoinsCount()}";
        return can;
    }

    public void UpdateText()
    {
        _textCoins.text = $"{CoinsModel.GetCoinsCount()}";
    }
}
