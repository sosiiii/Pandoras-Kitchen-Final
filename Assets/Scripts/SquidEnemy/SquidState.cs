using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidState : MonoBehaviour
{
    protected SquidBase enemy;

    public SquidState(SquidBase enemy)
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
