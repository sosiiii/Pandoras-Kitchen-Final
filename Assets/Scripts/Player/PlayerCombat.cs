using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using woska_scripts;

public class PlayerCombat : MonoBehaviour
{
    public Transform player;

    Animator anim;

    public Transform attackPoint;
    public float attackRange;
    public int attackDamage;
    public LayerMask enemyLayer;

    public bool attacking;
    public float attackDelay;

    void Start()
    {
        player = this.transform;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    public void EnemyDetection(InputAction.CallbackContext context)
    {
        if (context.performed && !attacking)
        {
            StartCoroutine(Attack());;
        }
    }

    private IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        Hit();
        attacking = true;
        yield return new WaitForSeconds(attackDelay);
        attacking = false;
    }

    public void Hit()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            var squidBase = enemy.GetComponent<SquidBase>();

            if (squidBase == null)
            {
                continue;
            }

            squidBase.Demaged(attackDamage, player);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }
}


