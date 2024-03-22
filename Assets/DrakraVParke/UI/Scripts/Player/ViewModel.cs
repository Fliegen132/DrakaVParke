using _2048Figure.Architecture.ServiceLocator;
using DrakraVParke.Player;
using UnityEngine;

public class ViewModel : IService
{
    private Player _player;
    private ViewHP _viewHP;
    public ViewModel(Player player)
    {
        _player = player;
        _viewHP = ServiceLocator.current.Get<ViewHP>();
    }

    public void UpdateHP()
    {
        float hp = (float)_player.GetHP() / 10;
        Debug.Log(hp);
        _viewHP.UpdateFill(hp);
    }
}
