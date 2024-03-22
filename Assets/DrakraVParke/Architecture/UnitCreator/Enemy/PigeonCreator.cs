using DrakaVParke.Architecture;
using DrakraVParke.Units;
using UnityEngine;

namespace DrakraVParke.Architecture
{
    public class PigeonCreator : ICreator
    {
        public override GameObject Create(Transform parent, string n)
        {
            var enemy = Resources.Load<GameObject>("Pigeon");
            GameObject go = Object.Instantiate(enemy, parent);
            Unit unit = go.GetComponent<Unit>();
            unit.Init(new Enemy("Pigeon", go));
            unit.GetBehaviour().SetHp(5);
            UnitList.Enemy.Add(unit);
            go.SetActive(false);
            return go;
        }
    }
}
