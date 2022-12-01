using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : State
{
    public CrouchState()
    {
        this.velocity = new Vector4(0, 0, 1, 1);
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        Debug.Log("Player Crouch");
    }
    public override bool Buffer(int frames)
    {
        return false;
    }

    public override void setup()
    {
        
    }
}
