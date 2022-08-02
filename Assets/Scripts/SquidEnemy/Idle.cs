using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : SquidState
{
    public Idle(SquidBase enemy) : base(enemy)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter idle");
    }
}
