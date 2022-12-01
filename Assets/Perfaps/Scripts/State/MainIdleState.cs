using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainIdleState : State
{

    public float Duration;
    protected Animator animator;
    protected HitEffect HitEffect;

    protected MeleeBaseState MeleeAttack;
    protected WalkLeftState WalkLeft;
    protected WalkRightState WalkRight;
    protected JumpState JumpUp;
    protected CrouchState Crouch;
    protected InputController input;

    private Condition currentCondition { get { return stateMachine.CurrentState.Condition; } }
    public MainIdleState()
    {
        velocity = Vector3.zero;
        MeleeAttack = new MeleeAttackState();
        WalkLeft = new WalkLeftState();
        WalkRight = new WalkRightState();
        JumpUp = new JumpState();
        Crouch = new CrouchState();
    }

    public override void setup()
    {
        //onGround
        stateMachine.inputController.AddInputCommand(new CInput[] {InputType.A}, new InputController.InputMove(DoMeleeAttack));
        stateMachine.inputController.AddInputCommand(new CInput[] {InputType.left}, new InputController.InputMove(DoWalkLeft));
        stateMachine.inputController.AddInputCommand(new CInput[] {InputType.right}, new InputController.InputMove(DoWalkRight));
        stateMachine.inputController.AddInputCommand(new CInput[] {InputType.neutral}, new InputController.InputMove(ReturnToIdle));
        stateMachine.inputController.AddInputCommand(new CInput[] {InputType.down}, new InputController.InputMove(DoCrouch));
        //inAir
        stateMachine.inputController.AddInputCommand(new CInput[] {InputType.up}, new InputController.InputMove(DoJump));
        MeleeAttack.setup();
        JumpUp.setup();
    }

    public override void OnEnter(StateMachine _stateMachine)
    {
        base.OnEnter(_stateMachine);
        this.MeleeAttack.stateMachine = _stateMachine;
        animator = _stateMachine.Animator;
        animator.Play("IdlePlayer");
        Debug.Log("Player idle");
        _stateMachine.MeleeHitBox.ResetHitBoxes();
        Owner.velocity.x = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }
    public bool DoWalkLeft(int frames)
    {
        if (currentCondition != Condition.neutral) return false;
        stateMachine.SetNextState(WalkLeft);
        return true;
    }
    public bool DoWalkRight(int frames)
    {
        if (currentCondition != Condition.neutral) return false;
        stateMachine.SetNextState(WalkRight);
        return true;
    }
    public bool DoJump(int frames)
    {
        if (currentCondition != Condition.neutral) return false;
        stateMachine.SetNextState(JumpUp);
        return true;
    }
    public bool DoCrouch(int frames)
    {
        if (currentCondition != Condition.neutral) return false;
        stateMachine.SetNextState(Crouch);
        return true;
    }
    public bool ReturnToIdle(int frames)
    {
        if (isActive||stateMachine.Condition!=Condition.neutral) return false;
        stateMachine.SetNextState(this);
        return true;
    }

    public bool DoMeleeAttack(int frames)
    {
        if (!this.isActive) return false;   
        stateMachine.SetNextState(MeleeAttack);
        Debug.Log("attack: " + frames);
        return true;
    }
    public override void OnExit()
    {
        base.OnExit();
    }

    public override bool Buffer(int frames)
    {
        return false;
    }
}
