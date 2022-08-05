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
        Debug.Log("EnteredPatroling");
    }
    public override void Process()
    {
        GroundedPosition = enemy.transform.position.y;
        if (enemy.patrolTimer <= 0)
        {
            enemy.canTimerRun = false;
            enemy.patrolTimer = enemy.patrolStartTimer;
            Patrol();
        }

        if (!enemy.canTimerRun)
        {
            PatrolMove();
        }
    }
    public override void Exit()
    {
        enemy.patroling = false;

    }

   /* IEnumerator Patroled()
    {
        Debug.Log("IEnuerator");
        movement = new Vector2(Random.Range(minMovement, maxMovement), GroundedPosition);
        Debug.Log(point1);
        Debug.Log(point2);
        if (movement.x < point2.x && movement.x > point1.x)
        {
            Debug.LogError("true");
            enemy.transform.position = new Vector2(movement.x, movement.y) * Time.deltaTime * enemy.patrolingSpeed;
            yield return new WaitForSeconds(enemy.patrolWait);
            StartCoroutine(Patroled());
        }
        else
        {
            Debug.LogError("false");
            StartCoroutine(Patroled());
        }
    }*/

    void Patrol()
    {
        movement = new Vector2(Random.Range(minMovement, maxMovement), GroundedPosition);
    }

    void PatrolMove()
    {
        Debug.Log(movement);
        if (movement.x < point2.x && movement.x > point1.x)
        {

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


        if (enemy.transform.position.x == movement.x)
        {
            movement.y = enemy.transform.position.y;
            enemy.canTimerRun = true;
        }
    }
}
