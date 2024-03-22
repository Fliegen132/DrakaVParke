using System.Collections;
using System.Threading.Tasks;
using DrakaVParke.Architecture;
using UnityEngine;

namespace DrakraVParke.Architecture
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;

        private int _currentWave;
        private bool startSpawn = false;
        private void Update()
        {
            bool allEnemiesInactive = true;
            foreach (var enemy in UnitList.Enemy)
            {
                if (enemy.gameObject.activeSelf)
                {
                    allEnemiesInactive = false;
                    break;
                }

            }
            if(startSpawn)
                return;
            if (allEnemiesInactive)
            {
                startSpawn = true;
                _currentWave = Mathf.Min(_currentWave + 1, 5);
                StartCoroutine(SpawnEnemies());
                
            }
        }

        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < _currentWave; i++)
            {
                if (i < UnitList.Enemy.Count && !UnitList.Enemy[i].gameObject.activeSelf)
                {
                    yield return new WaitForSeconds(1f);
                    int pos = Random.Range(0, _spawnPoints.Length);
                    UnitList.Enemy[i].transform.position =
                        new Vector3(_spawnPoints[pos].position.x, _spawnPoints[pos].position.y, UnitList.Enemy[i].transform.localPosition.z);
                    UnitList.Enemy[i].gameObject.SetActive(true);
                }
            }

            startSpawn = false;
        }
    }
}