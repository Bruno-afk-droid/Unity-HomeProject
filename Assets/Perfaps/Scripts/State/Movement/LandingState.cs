using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingState : State
{
    private State airborn;
    private int landingLag = 4;
    public LandingState(State airborn)
    {
        this.airborn = airborn;
        Condition = Condition.airborn;
        velocity = new Vector3(0, 0, 1);
    }
    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        stateMachine.Animator.Play("JumpLanding");
        landingLag = 3;
        Debug.Log("Player Landing");
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (Owner.IsGrounded)
        {
            if (landingLag > 0) landingLag--; else
            {
                stateMachine.SetNextStateToMain();
                stateMachine.inputController.RedoInput();
            }
        }
        else
        {
            stateMachine.SetNextState(airborn);
        }
    }
    public override bool Buffer(int frames)
    {
        return false;
    }
    public override void setup()
    {

    }
}
