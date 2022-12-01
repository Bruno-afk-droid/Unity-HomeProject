using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : MeleeBaseState 
{
    public MeleeAttackState()
    {
        HitEffects = new HitEffect[] { new HitEffect(10,10,Vector2.zero,1)};
    }
    public MeleeAttackState(HitEffect[] hitEffects)
    {
        HitEffects = hitEffects;
    }

    public override void setup()
    {
        Condition = Condition.attack;
        stateMachine.inputController.AddInputCommand(new CInput[] {InputType.A}, new InputController.InputMove(Buffer));
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);

        //Attack
        attackIndex = 1;
        Duration = (1.00f/60)*10;
        MeleeHitBox.SetHitBoxes(HitEffects);
        animatior.Play("Jab");
        //Debug.Log("Player Attack " + attackIndex + " Fired");
    }
    public override bool Buffer(int frames)
    {
        if (!this.isActive) return false;
        this.BufferdState = this;
        Debug.Log("bufferd: "+frames);
        return true;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        this.MeleeHitBox.CheckMelee();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (fixedTime >= Duration)
        {
            if (shouldCombo)
            {
                stateMachine.SetNextState(this);
            }
            else
            {
                stateMachine.GoToNextState();
            }
        }
    }
}
