using System.Collections;
using UnityEngine;

public class Patroling : SquidState
{
    Vector2 point1;
    Vector2 point2;

    float minMovement = -10;
    float maxMovement = 10;
    Vector2 movement;

    public float timer;

    public float GroundedPosition;

    public float ErrorTimer;

    public Patroling(SquidBase enemy) : base(enemy)
    {

    }

    public override void Enter()
    {
        GroundedPosition = enemy.transform.position.y;
        enemy.patrolTimer = 0;
        point1 = enemy.patrol1;
        point2 = enemy.patrol2;
        enemy.patroling = true;
        Debug.LogError("EnteredPatroling");
    }
    public override void Process()
    {
        GroundedPosition = enemy.transform.position.y;
        if (enemy.patrolTimer <= 0)
        {
            //Debug.LogError("CanTimerRunFalse");
            enemy.canTimerRun = false;
            enemy.patrolTimer = Random.Range(1, 5);
            Patrol();
        }

        if (!enemy.canTimerRun)
        {
            PatrolMove();
        }
        ErrorTimer += Time.fixedDeltaTime;
    }
    public override void Exit()
    {
        enemy.patroling = false;
        Debug.LogError("PatrolingExit");
    }

    void Patrol()
    {
        movement = new Vector2(Random.Range(minMovement, maxMovement), GroundedPosition);
        enemy.patrolingSpeed = Random.Range(enemy.patrolingSpeedMin, enemy.patrolingSpeedMax);
    }

    void PatrolMove()
    {
        //Debug.Log(movement);
        if (movement.x < point2.x && movement.x > point1.x)
        {
            //Debug.LogError("Moving");
            if (movement.x >= enemy.transform.position.x)
            {
                enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (movement.x <= enemy.transform.position.x)
            {
                enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, movement, enemy.patrolingSpeed);
        }
        else
        {
            //Debug.LogError("FalseIf");
            Patrol();
        }

        if (ErrorTimer >= 10)
        {
            Patrol();
            ErrorTimer = 0;
        }

        if (enemy.transform.position.x == movement.x)
        {
            movement.y = enemy.transform.position.y;
            //Debug.LogError("CanTimerRunTrue");
            enemy.canTimerRun = true;
            ErrorTimer = 0;
        }
    }
}
