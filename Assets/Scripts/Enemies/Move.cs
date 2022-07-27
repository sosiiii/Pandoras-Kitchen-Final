using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : State
{
    Player player;

    float jumpForce;
    bool grounded;
    public override void Enter()
    {
        Debug.Log("Enter Move");
        Delay();
    }
    IEnumerator Delay()
    {

        yield return new WaitForSeconds(Random.Range(1, 5));
        yield return Delay();
    }
    public override void Process()
    {
        float direction = enemy.transform.position.x - player.transform.position.x;

        enemy.rb.velocity = new Vector3(direction, 0).normalized * enemy.speed;

    }

    public Move(EnemyBase enemy, Player player) : base(enemy)
    {
        this.player = player;
        this.jumpForce = enemy.jumpForce;
        this.grounded = enemy.grounded;
    }

    public override void Exit()
    {
        enemy.rb.velocity = Vector3.zero;
    }
}
