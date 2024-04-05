using System;
using System.Collections.Generic;
using DrakaVParke.Architecture;
using DrakraVParke.Architecture.Menu;
using DrakraVParke.Units;
using InstantGamesBridge;
using UnityEngine;
using UnityEngine.Rendering;

namespace DrakraVParke.Architecture
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private Transform enemyPool;

        private void Awake()
        {
        }

        private void Start()
        {
            Bridge.advertisement.ShowBanner();
            UnitList.Enemy = new List<Unit>();
            UnitList.Player = null;
            UnitList.EnemyBall = null;
            if (DataMenu.HeroName == "Cat")
            {
                ICreator playerCreator = new CatCreator();
                playerCreator.Create(null, DataMenu.HeroSkin);
            }

            if (DataMenu.HeroName == "Babushka")
            {
                ICreator playerCreator = new BabushkaCreator();
                playerCreator.Create(null, DataMenu.HeroSkin);
            }
          
            ICreator enemy = new RatCreator();
            ICreator enemy2 = new PigeonCreator();
            int layer = 3;
            for (int i = 0; i < 6; i++)
            {
                GameObject go = enemy2.Create(enemyPool, "");
                go.GetComponent<SortingGroup>().sortingOrder = layer;
                
                GameObject go2 = enemy.Create(enemyPool, "");
                go2.GetComponent<SortingGroup>().sortingOrder = layer + 1 ;
                layer++;
            }
            
           
        }
    }
}