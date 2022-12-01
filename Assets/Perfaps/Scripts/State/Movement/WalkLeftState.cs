using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkLeftState : State
{
    public WalkLeftState()
    {
        velocity = new Vector4(-5, 0, 1, 0);
    }

    public override bool Buffer(int frames)
    {
        return false;
    }

    public override void OnEnter(StateMachine _stateMachine) 
    {
        base.OnEnter(_stateMachine);
        Debug.Log("Player leftRight");
    }

    public override void setup()
    {
    }
}
