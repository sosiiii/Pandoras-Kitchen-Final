using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int maxEnemies = 4;

    [SerializeField] private float minSpawnTime = 5f;
    [SerializeField] private float maxSpawnTime = 10f;
    
    private int _enemiesAlive = 0;

    private WaitForSeconds _waitForSeconds;

    void Start()
    {
        _waitForSeconds = new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime)); 
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitUntil(()=>_enemiesAlive < maxEnemies);
            var enemy = Instantiate(enemyPrefab, transform.position, quaternion.identity);

            enemy.GetComponent<IOnDeath>().DeathAction += OnEnemyDeath;
            _enemiesAlive++;
            
            yield return _waitForSeconds;
        }
    }


    private void OnEnemyDeath()
    {
        _enemiesAlive--;
    }
}
