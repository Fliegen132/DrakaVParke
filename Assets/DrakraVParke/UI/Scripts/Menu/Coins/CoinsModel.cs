
using System;
using InstantGamesBridge;

public static class CoinsModel 
{
    private static int _coinsCount;

    public static void Init()
    {
        Bridge.storage.Get("money", IOnStorageGetCompleted);
    }
    private static void IOnStorageGetCompleted(bool success, string data)
    {
        if (success)
        {
            if (data != null) {
                _coinsCount = Int32.Parse(data);
            }
        }
    }
    public static bool SendCoin(int coins)
    {
        if (_coinsCount < coins)
        {
            return false;

        }
        _coinsCount -= coins;
        return true;
    }

    public static void AddCoins(int value)
    {
        _coinsCount += value;
    }
    
    
    public static int GetCoinsCount() => _coinsCount;
}
