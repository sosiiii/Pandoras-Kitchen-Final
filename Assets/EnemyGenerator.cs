using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{

    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private int maxEnemies = 4;

    private int _enemiesAlive = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitUntil(()=>_enemiesAlive < maxEnemies);
            Instantiate(enemyPrefab, transform.position, quaternion.identity);
            _enemiesAlive++;
            yield return new WaitForSeconds(Random.Range(8f, 16f));   
        }
    }


    private void OnEnemyDeath()
    {
        _enemiesAlive--;
    }
}
