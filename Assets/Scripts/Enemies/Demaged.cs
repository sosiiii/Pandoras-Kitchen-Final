using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demaged : State
{
    int health;

    public override void Enter()
    {
        Debug.Log("Enter Demaged");
    }


    public Demaged(EnemyBase enemy) : base(enemy)
    {
    }

    public override void Process()
    {
        //DemagedEnemy(int Demage);
    }

    void DemagedEnemy(int demage)
    {

    }
}
