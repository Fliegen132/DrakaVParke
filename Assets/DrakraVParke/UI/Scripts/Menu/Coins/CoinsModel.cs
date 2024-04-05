
using System;
using InstantGamesBridge;
using UnityEngine;

public static class CoinsModel 
{
    private static int _coinsCount;

    public static void Init()
    {
        _coinsCount = PlayerPrefs.GetInt("money");
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
