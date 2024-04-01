using System;
using System.Collections;
using System.Threading.Tasks;
using DrakaVParke.Architecture;
using DrakraVParke.Architecture.Menu;
using DrakraVParke.Player;
using DrakraVParke.Units;
using DrakraVParke.Units.Ball;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DrakraVParke.Architecture
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPoints;

        private int _currentWave;
        private bool startSpawn = false;
        [SerializeField] public GameObject light;
        [SerializeField] private Tutorial _tutorial;
        private float _maxTimeForBall = 10;
        private float _currentTime;
        private int _currentEnemy;
        [SerializeField] private HeartBehaviour[] _heart;
        public void Start()
        {
            if (DataMenu.Night)
            {
                light.SetActive(true);
            }
        }

        private void Update()
        {
            if(!_tutorial.isDone)
                return;
            BallBehaviour();
            
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

        
        private void BallBehaviour()
        {
            if (_currentTime < _maxTimeForBall)
            {
                _currentTime += Time.deltaTime;
            }
            else
            {
                ICreator ballEnemy = new BallCreator();
                GameObject go = ballEnemy.Create(null, "BallRat");
                UnitList.EnemyBall.transform.GetChild(0).transform.GetChild(0).GetComponent<Unit>()
                    .GetBehaviour().Init();
                go.SetActive(true);
                _currentTime = 0;
            }
        }
        
        private IEnumerator SpawnEnemies()
        {
            for (int i = 0; i < _currentWave; i++)
            {
                if (i < UnitList.Enemy.Count && !UnitList.Enemy[i].gameObject.activeSelf)
                {
                    int time = Random.Range(1, 4);
                    if(UnitList.Enemy[i].gameObject.GetComponent<HeartCarrier>())
                        Destroy(UnitList.Enemy[i].gameObject.GetComponent<HeartCarrier>());
                    
                    _currentEnemy++;
                    yield return new WaitForSeconds(time);
                    int pos = Random.Range(0, _spawnPoints.Length);
                    UnitList.Enemy[i].transform.position =
                        new Vector3(_spawnPoints[pos].position.x, _spawnPoints[pos].position.y, UnitList.Enemy[i].transform.localPosition.z);
                    UnitList.Enemy[i].gameObject.SetActive(true);
                    UnitList.Enemy[i].gameObject.GetComponent<SkinRandomize>()?.Init();
                    UnitList.Enemy[i].gameObject.transform.Find("Fade").gameObject.SetActive(true);
                    if (_currentEnemy >= 5)
                    {
                        foreach (var heart in _heart)
                        {
                            if (!heart.gameObject.activeInHierarchy)
                            {
                                UnitList.Enemy[i].gameObject.AddComponent<HeartCarrier>().SetHeart(heart.gameObject);
                                _currentEnemy = 0; 
                                break;
                            }
                        }
                        
                    }
                }
            }

            startSpawn = false;
        }
    }
}