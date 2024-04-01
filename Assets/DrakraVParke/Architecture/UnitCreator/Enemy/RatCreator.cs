using DrakaVParke.Architecture;
using DrakraVParke.Architecture;
using DrakraVParke.Units;
using UnityEngine;

public class RatCreator : ICreator
{
    public override GameObject Create(Transform parent, string n)
    {
        var enemy = Resources.Load<GameObject>("Rat");
        GameObject go = Object.Instantiate(enemy, parent);
        Unit unit = go.GetComponent<Unit>();
        unit.Init(new Enemy("Rat", go));
        unit.GetBehaviour().SetHp(5);
        UnitList.Enemy.Add(unit);
        go.SetActive(false);
        return go;
    }
}
