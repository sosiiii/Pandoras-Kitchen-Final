using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public Transform player;

    public Animator anim;

    public Transform attackPoint;
    public float attackRange;
    public int attackDemage;
    public LayerMask enemyLayer;

    public GameObject[] ammoPoints;
    public int ammo;
    public int maxAmmo;
    public bool reload;
    
    private float time = 2;
    private bool relode = false;
    

    void Start()
    {
        player = this.transform;
        anim = GetComponent<Animator>();
        ammo = maxAmmo;
    }

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
            time = 2;
            ammo++;
        }
        /*
        if (ammo == 3)
        {
            ammoPoints[2].gameObject.SetActive(true);
            ammoPoints[1].gameObject.SetActive(true);
            ammoPoints[0].gameObject.SetActive(true);
        }
        else if (ammo == 2)
        {
            ammoPoints[2].gameObject.SetActive(false);
            ammoPoints[1].gameObject.SetActive(true);
            ammoPoints[0].gameObject.SetActive(true);
        }
        else if (ammo == 1)
        {

            ammoPoints[2].gameObject.SetActive(false);
            ammoPoints[1].gameObject.SetActive(false);
            ammoPoints[0].gameObject.SetActive(true);
        }
        else if (ammo == 0)
        {

            ammoPoints[2].gameObject.SetActive(false);
            ammoPoints[1].gameObject.SetActive(false);
            ammoPoints[0].gameObject.SetActive(false);
        }

        if (ammo < maxAmmo)
        {
            StartCoroutine(Reloading());
        }*/
    }
    
    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(2);
    }

    public void EnemyDetection(InputAction.CallbackContext context)
    {
       
        if (context.performed && ammo > 0)
        {
            ammo--;
            anim.SetTrigger("Attack");
            // StopCoroutine(Reloading());
            // StartCoroutine(Reloading());
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
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }
}


