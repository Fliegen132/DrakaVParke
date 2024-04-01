using DrakaVParke.Architecture;
using DrakraVParke.Units;
using DrakraVParke.Units.Ball;
using UnityEngine;

namespace DrakraVParke.Architecture
{
    public class BallCreator : ICreator
    {
        public override GameObject Create(Transform parent, string skinName)
        {
            var enemy = Resources.Load<GameObject>(skinName);
            GameObject go = Object.Instantiate(enemy, parent);
            go.GetComponent<Ball>().Init();
            Unit unit = go.transform.GetChild(0).transform.GetChild(0).GetComponent<Unit>();
            unit.Init(new EnemyBall(unit.gameObject, "Rat"));
            unit.GetBehaviour().SetHp(5);
            Debug.Log(go.name);
            UnitList.EnemyBall = go;
            go.SetActive(false);
            return go;
        }
    }
}