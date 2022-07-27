using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public Idle(EnemyBase enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter idle");
    }
}
