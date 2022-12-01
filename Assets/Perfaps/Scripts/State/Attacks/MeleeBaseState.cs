    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBaseState : State
{
    //mainValues
    public float Duration;
    protected bool shouldCombo;

    //MeleeAttackinfo
    protected HitEffect[] HitEffects;
    protected int attackIndex;
    

    //refrences
    protected MeleeHitBox MeleeHitBox;
    protected Animator animatior;

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        animatior = _stateMachine.Animator;
        MeleeHitBox = _stateMachine.MeleeHitBox;
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public override void setup()
    {
    }

    public override bool Buffer(int frames)
    {
        return false;
    }
}
