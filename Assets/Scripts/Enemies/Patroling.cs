using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroling : State
{
    float speed = 10;

    public List <Vector2> points = new List<Vector2>();

    int destination;

    float patrolTime;

    List<Rigidbody2D> holdingObjects;

    public override void Enter()
    {
        this.patrolTime = enemy.patrolWait;
        Debug.Log("EnterPatroling");
        points.Clear();
        foreach (Transform point in enemy.transform)
        {
            RaycastHit2D hit = Physics2D.Raycast(enemy.rb.position, -point.localPosition.normalized, 10, enemy.PatrolLayerMask);
            if (hit.collider != null)
            {
                Debug.Log(Vector2.Distance(hit.point, enemy.rb.position));
                points.Add(hit.point);
            }
            else
            {
                Debug.Log("Else");
                points.Add(point.position);
            }
        }
    }

    public override void Process()
    {
        Debug.Log(Vector2.Distance(enemy.rb.position, (Vector2)points[destination]));

        if (Vector2.Distance(enemy.rb.position, (Vector2)points[destination]) <= 2)
        {
            patrolTime -= Time.fixedDeltaTime;
            if (patrolTime <= 0)
            {
                destination = (destination + 1) % points.Count;
                if (destination == 1)
                {
                    enemy.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                patrolTime = enemy.patrolWait;
            }

        }
        else
        {
            var diff = (Vector2)points[destination] - enemy.rb.position;
            enemy.rb.MovePosition(enemy.rb.position + diff.normalized * Time.fixedDeltaTime * speed);
        }
    }

    public Patroling(EnemyBase enemy) : base(enemy)
    {

    }
}
