using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirBornState : State
{
    public State landing;
    public AirBornState(State landing)
    {
        this.landing = landing;
        Condition = Condition.airborn;
        velocity = new Vector3(0, 0, 0);
        DI_vel = new Vector2(2, 2);
    }

    public override bool Buffer(int frames)
    {
        return false;
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        stateMachine.Animator.Play("AirFall");
        Debug.Log("Player airborn");
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        if (Owner.IsGrounded)
        {
            stateMachine.SetNextState(landing);
        }
        else
        {
            ApplyDI(stateMachine.inputController.DI);
        }
    }

    public override void setup()
    {
        throw new System.NotImplementedException();
    }
}
