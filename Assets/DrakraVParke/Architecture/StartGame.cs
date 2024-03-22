using System;
using DrakraVParke.Architecture.Menu;
using UnityEngine;
using UnityEngine.Rendering;

namespace DrakraVParke.Architecture
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField] private Transform enemyPool;
        private void Start()
        {
            if (DataMenu.heroName == "Cat")
            {
                ICreator playerCreator = new CatCreator();
                playerCreator.Create(null, DataMenu.heroSkin);
            }

            if (DataMenu.heroName == "Babushka")
            {
                ICreator playerCreator = new BabushkaCreator();
                playerCreator.Create(null, DataMenu.heroSkin);
            }

            ICreator enemy = new PigeonCreator();
            int layer = 3;
            for (int i = 0; i < 3; i++)
            {
                GameObject go = enemy.Create(enemyPool, "");
                go.GetComponent<SortingGroup>().sortingOrder = layer;
                layer++;
            }

        }
    }
}