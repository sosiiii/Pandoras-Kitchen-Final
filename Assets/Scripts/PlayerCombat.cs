using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Transform player;

    public Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public int attackDemage;
    public LayerMask enemyLayer;

    void Start()
    {
        player = this.transform;
    }

    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.F))
        {
            //animator.SetTrigger("Attack");
            EnemyDetection();
        }*/
    }
    


    public void EnemyDetection(InputAction.CallbackContext context)
    {
       
        if (context.performed)
        {
            
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("We hit " + enemy.name);
                enemy.GetComponent<SquidBase>().Demaged(attackDemage, player);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }
}


