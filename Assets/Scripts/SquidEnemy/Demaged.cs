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
    public override void Enter()
    {
        Debug.Log("Enter Demaged");

        rb = enemy.rb;

        enemy.health -= damage;
        KnockBack();
        if (enemy.health <= 0)
        {
            Stun();
        }
        else
        {
            enemy.Move(player);
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

    void Stun()
    {
        enemy.Stunned();
    }

    void KnockBack()
    {
        Vector2 knockbackDirection = new Vector2(enemy.transform.position.x - player.position.x, 0);
        rb.velocity = new Vector2(knockbackDirection.x * knockbackForce , knockbackForceUp);
    }
}
