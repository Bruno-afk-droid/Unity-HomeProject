using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State
{
    private State airBornState;
    private State landingState;
    public JumpState()
    {
        landingState = new LandingState(airBornState);
        airBornState = new AirBornState(landingState);
        Condition = Condition.airborn;
        velocity = new Vector4(0, 10, 0,1);
        DI_vel = new Vector2(2, 2);
    }

    public override bool Buffer(int frames)
    {
        throw new System.NotImplementedException();
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        stateMachine.Animator.Play("JumpStart");
        Debug.Log("Player jump");      
    }
    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (!Owner.IsGrounded)
        {
            ApplyDI(stateMachine.inputController.DI);
            if (this.Owner.velocity.y<0)
            {
                    stateMachine.SetNextState(airBornState);
            }
        }
        else
        {
            stateMachine.SetNextState(landingState);
        }
    }

    public override void setup()
    {

    }
}
