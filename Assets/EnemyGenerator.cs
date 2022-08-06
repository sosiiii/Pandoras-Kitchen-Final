using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : MonoBehaviour
{


    [SerializeField] private SquidBase enemy;
    [SerializeField] private int maxEnemies = 4;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
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
