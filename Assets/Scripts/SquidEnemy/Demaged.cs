using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demaged : SquidState
{
    int damage;

    public float knockbackForce;
    public float knockbackForceUp;

    Transform player;

    Rigidbody2D rb;

    Animator anim;
    public override void Enter()
    {
        anim = enemy.GetComponent<Animator>();
        Debug.Log("Enter Demaged");

        rb = enemy.rb;
        anim.SetTrigger("Demaged");
        enemy.health -= damage;
        KnockBack();
        if (enemy.health <= 0)
        {
            Stun();
        }
    }


    public Demaged(SquidBase enemy, int damage, Transform player, float knockbackForce, float knockbackForceUp) : base(enemy)
    {
        this.damage = damage;
        this.player = player;
        this.knockbackForce = knockbackForce;
        this.knockbackForceUp = knockbackForceUp;
    }

    public override void Process()
    {
        //DemagedEnemy(int Demage);
    }

    IEnumerator Stun()
    {


        yield return null;
    }

    void KnockBack()
    {
        Vector2 knockbackDirection = new Vector2(enemy.transform.position.x - player.position.x, 0);
        rb.velocity = new Vector2(knockbackDirection.x, knockbackForceUp) * knockbackForce;
    }
}
