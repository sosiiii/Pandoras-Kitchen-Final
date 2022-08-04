using System.Collections;
using UnityEngine;

public class Patroling : SquidState
{
    Vector2 point1;
    Vector2 point2;

    float minMovement = -10;
    float maxMovement = 10;
    float movement;

    public float timer;

    public Patroling(SquidBase enemy) : base(enemy)
    {

    }

    public override void Enter()
    {
        point1 = enemy.patrol1;
        point2 = enemy.patrol2;
        StartCoroutine(Patrol());
        enemy.patrolTimer = enemy.patrolStartTimer;
        enemy.patroling = true;
    }
    public override void Process()
    {

    }
    public override void Exit()
    {
        enemy.patroling = false;

    }

    IEnumerator Patrol()
    {
        Debug.Log("IEnuerator");
        movement = Random.Range(minMovement, maxMovement);
        Debug.Log(point1);
        Debug.Log(point2);
        if (movement < point2.x && movement > point1.x)
        {
            Debug.LogError("true");
            enemy.transform.position = new Vector2(movement, enemy.transform.position.y) * Time.deltaTime * enemy.patrolingSpeed;
            yield return new WaitForSeconds(enemy.patrolWait);
            StartCoroutine(Patrol());
        }
        else
        {
            Debug.LogError("false");
            StartCoroutine(Patrol());
        }
    }

    void Patroled()
    {
        if (timer <= 0)
        {
            movement = Random.Range(minMovement, maxMovement);
            if (movement < point2.x && movement > point1.x)
            {
                Debug.LogError("true");
                enemy.transform.position = new Vector2(movement, enemy.transform.position.y) * Time.deltaTime * enemy.patrolingSpeed;
                enemy.patrolTimer = enemy.patrolStartTimer;
                Patroled();
            }
            else
            {
                Debug.LogError("false");
                Patroled();
            }
        }
        else
        {
            Patroled();
        }
    }
}
