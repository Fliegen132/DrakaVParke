using DrakaVParke.Architecture;
using DrakaVParke.Player;
using DrakraVParke.Units;
using UnityEngine;

namespace DrakraVParke.Architecture
{
    public class BabushkaCreator : ICreator
    {
        public override GameObject Create(Transform parent, string skin)
        {
            var player = Resources.Load<GameObject>(skin);
            GameObject go = Object.Instantiate(player);
            Unit unit = go.AddComponent<Unit>();
            IInput input = new DesktopInput(new AnimationController(go.GetComponent<Animator>()), unit);
            unit.Init(new Player.Player("Babushka",go, input));
            
            unit.GetBehaviour().SetHp(10);
            UnitList.Player = go;
            return go;
        }
    }
}
