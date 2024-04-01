using DrakaVParke.Architecture;
using DrakaVParke.Player;
using DrakraVParke.Units;
using InstantGamesBridge;
using UnityEngine;
using DeviceType = InstantGamesBridge.Modules.Device.DeviceType;

namespace DrakraVParke.Architecture
{
    public class BabushkaCreator : ICreator
    {
        public override GameObject Create(Transform parent, string skin)
        {
            IInput input;
            var player = Resources.Load<GameObject>(skin);
            GameObject go = Object.Instantiate(player);
            Unit unit = go.AddComponent<Unit>();
            go.AddComponent<AudioSource>().playOnAwake = false;
            
            if (Bridge.device.type == DeviceType.Mobile)
            {
                input = new MobileInput(unit,new AnimationController(go.GetComponent<Animator>()));
                Debug.Log("mobile");
            }
            else
            {
                input = new DesktopInput(unit,new AnimationController(go.GetComponent<Animator>()));
                Debug.Log("desktop");
            }
            unit.Init(new Player.Player("Babushka",go, input));
            unit.GetBehaviour().SetHp(10);
            UnitList.Player = go;
            return go;
        }
    }
}
