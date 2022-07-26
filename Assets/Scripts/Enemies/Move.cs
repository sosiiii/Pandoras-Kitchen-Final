using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : State
{
    Player player;
    public override void Enter()
    {
        Debug.Log("Enter Move");
    }

    public override void Process()
    {
        float direction = enemy.transform.position.x - player.transform.position.x;
        enemy.rb.velocity = new Vector3(direction, 0).normalized * enemy.speed;
    }

    public Move(EnemyBase enemy, Player player) : base(enemy)
    {
        this.player = player;
    }

    public override void Exit()
    {
        enemy.rb.velocity = Vector3.zero;
    }
}
