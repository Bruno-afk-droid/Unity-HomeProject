using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundComboState : MeleeBaseState
{

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        attackIndex = 2;
        Duration = 0.5f;
        animatior.SetTrigger("Attack" + attackIndex);
        Debug.Log("Player Attack " + attackIndex + " Fired");
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= Duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(new GroundFinisherState());
            }
            else
            {
                stateMachine.SetNextStateToMain();
            }
        }
    }

}
