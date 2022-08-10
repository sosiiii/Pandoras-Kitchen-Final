using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth;

    [Header("Items")]
    [SerializeField] private Item deadEnemyItem;
    [SerializeField] private ItemObject itemObjectPrefab;

    void Update()
    {
        Death();
    }

    private void Death()
    {
        if (enemyHealth <= 0)
        {
            var itemObject = Instantiate(itemObjectPrefab, transform.position, Quaternion.identity);

            itemObject.Init(deadEnemyItem);
            Destroy(gameObject);

        }
    }

    public void EnemyTakeDamage(int damage)
    {
        enemyHealth -= damage;
    }
}
