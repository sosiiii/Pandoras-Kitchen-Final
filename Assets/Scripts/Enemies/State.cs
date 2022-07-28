using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected EnemyBase enemy;

    public State(EnemyBase enemy)
    {
        this.enemy = enemy;
    } 

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {

    }

    public virtual void Process()
    {
        
    }

    public virtual void Updating()
    {

    }
}
