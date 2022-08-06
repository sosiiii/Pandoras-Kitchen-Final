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
    public int attackDemage;
    public LayerMask enemyLayer;

    public bool attacking;

    void Start()
    {
        player = this.transform;
        anim = GetComponent<Animator>();
        ammo = maxAmmo;
    }

    public GameObject[] ammoPoints;
    public int ammo;
    public int maxAmmo;
    public bool reload;

    private float time = 1.2f;
    private bool relode = false;

    public 
    void Update()
    {

        for (int i = 0; i < ammoPoints.Length; i++)
        {
            ammoPoints[i].SetActive(i < ammo);
        }



        if (relode)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0)
        {


            if (ammo == maxAmmo)
            {
                relode = false;
            }
            time = 1.2f;
            ammo++;
        }
    }



    public void EnemyDetection(InputAction.CallbackContext context)
    {
        if (context.performed && ammo > 0 && player.GetComponent<PlayerInteract>().ItemSlot.IsFull() == false)
        {
            attacking = true;
            Debug.Log("You see");
            ammo--;
            anim.SetTrigger("Attack");
            Hit();
            relode = false;
            relode = true;
            time = 2;
        }
    }

    public void Hit()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            enemy.GetComponent<SquidBase>().Demaged(attackDemage, player);
            
        }
        attacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }
}


