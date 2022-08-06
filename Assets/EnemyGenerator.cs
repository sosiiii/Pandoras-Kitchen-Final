using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private SquidBase enemy;
    [SerializeField] private int maxEnemies = 4;
    
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitUntil(()=>SquidBase.enemiesCount < maxEnemies);
            Instantiate(enemy, transform.position, quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8f, 16f));   
        }
    }
}
