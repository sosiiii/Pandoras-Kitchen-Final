using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : State
{
    Player player;

    float TimerReset;
    public float Timer;
    public float minTime;
    public float maxTime;

    float jumpForce;
    bool grounded;
    public override void Enter()
    {
        Debug.Log("Enter Move");
        enemy.MoveState = true;
    }


    public Move(EnemyBase enemy, Player player) : base(enemy)
    {
        this.player = player;
        this.jumpForce = enemy.jumpForce;
        this.grounded = enemy.grounded;
        this.Timer = enemy.Timer;
    }

    public override void Process()
    {
        Debug.LogError(enemy);
        Debug.Log(player);
        float direction = enemy.transform.position.x - player.transform.position.x;

        this.Timer = enemy.Timer;
        enemy.rb.velocity = new Vector3(direction, enemy.rb.velocity.y).normalized * enemy.speed;
        
        if (Timer <= 0 && grounded)
        {
            enemy.rb.AddForce(Vector2.up * jumpForce);

            enemy.Timer = Random.Range(enemy.minTime, enemy.maxTime);
            Debug.Log("Timer set");
        }

        if (direction > 0)
        {
            enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            //doprava
        }
        else if(direction < 0)
        {
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            //dolava
        }
    }


    public override void Exit()
    {
        enemy.rb.velocity = Vector3.zero;
        enemy.MoveState = false;
    }
}
