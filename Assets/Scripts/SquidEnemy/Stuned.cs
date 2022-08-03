using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuned : SquidState
{
    Transform EnemyPosition;
    public Stuned(SquidBase enemy) : base(enemy)
    {
        
    }

    public override void Enter()
    {
        EnemyPosition = enemy.transform;
    }

    public override void Process()
    {
        Instantiate(enemy.StunedEnemy, EnemyPosition.position, Quaternion.identity);
        Destroy(enemy.gameObject);
    }
    public override void Exit()
    {
        
    }
}
