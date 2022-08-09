using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : SquidState
{
    Transform player;

    public float direction;

    public Move(SquidBase enemy, Transform player) : base(enemy)
    {
        this.player = player;
    }

    public override void Enter()
    {
        direction = enemy.transform.position.x - player.transform.position.x;
        
    }

    public override void Process()
    {

        enemy.rb.velocity = new Vector2(direction, enemy.rb.velocity.y).normalized * enemy.speed;

        RaycastHit2D groundInfo = Physics2D.Raycast(enemy.RayShooter.position, Vector2.down, 2f);

        if(groundInfo == false)
        {
            direction -= 2 * direction;
        }
        else
        {
            direction += 2 * direction;
        }

    }

    public override void Exit()
    {
        
    }

    /*Player player;

    float TimerReset;
    public float Timer;
    public float minTime;
    public float maxTime;
    private float direction;

    float jumpForce;
    bool grounded;
    public override void Enter()
    {
        Debug.Log("Enter Move");
        enemy.MoveState = true;


        enemy.rb.velocity = new Vector2(direction, enemy.rb.velocity.y).normalized * enemy.speed;
    }

    public Move(SquidBase enemy, Player player) : base(enemy)
    {
        this.player = player;
        this.jumpForce = enemy.jumpForce;
        this.grounded = enemy.grounded;
        this.Timer = enemy.Timer;
    }

    public override void Process()
    {
q
        direction = enemy.transform.position.x - player.transform.position.x;

        //this.Timer = enemy.Timer;
        /*if (Timer <= 0 && grounded)
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
    }*/
}
