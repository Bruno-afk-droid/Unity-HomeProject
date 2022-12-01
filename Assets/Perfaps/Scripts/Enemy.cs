using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalLibriary;
using ActionInfo_;
using UnityEditor;


public class Enemy : touchable
{


    //buffer input
    public List<string> ActionBuffer = new List<string>();
    public int InputDelay = 4;

    public float MovementSpeed;
    public AnimationCurve WalkArk;
    public AnimationCurve JumpArk;
    public bool Jumping = false;

    //Animation
    private Animator Animator;
    public PlayerState PlayerState = PlayerState.Idle;

    public int CurrentFrame = 0;
    public int FinalFrame = 0;
    protected Vector2 Momentum;
    protected Vector2 VelForce;

    //states
    public StateMachine StateMachine;

    public override void Awake()
    {
        base.Awake();
        StateMachine = this.GetComponent<StateMachine>();
        Animator = this.GetComponent<Animator>();
        Animator.speed = 0.85f;
    }
}

