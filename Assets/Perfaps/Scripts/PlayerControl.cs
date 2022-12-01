using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalLibriary;
using ActionInfo_;
using UnityEditor;

public enum PlayerState
{
    landing=-1,
    Idle=0,
    Walking=1,
    Jumping=2,
    Attack=3,
}

public class PlayerControl : Player
{
    //input
    public InputController InputController;

    public float MovementSpeed;
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
        InputController = this.GetComponent<InputController>();
        StateMachine = this.GetComponent<StateMachine>();
        Animator = this.GetComponent<Animator>();

        //InputController.AddInputCommand(new InputType[]{InputType.down, InputType.right, InputType.A},new InputController.InputMove(test));

        Animator.speed = 0.85f;
        //Dictionary<int, CollisionBox[]> dict = new Dictionary<int, CollisionBox[]>();
        //dict.Add(0,new CollisionBox[]{new CollisionBox(new Rect(0,0,30,60),new Effect())});
        //Actioninfos.Add("test", new ActionInfo(dict,1));

    }
    protected override void Start()
    {
        StateMachine.setup();
    }

    // Start is called before the first frame update
    protected override void FixedUpdate()
    {
        //if (InputController.getInput(new InputType[] { InputType.down, InputType.right, InputType.A })) Debug.Log("motion input");

        //Vector2 DirectionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //bool JumpInput = DirectionalInput.y>0;

        //this.FPMultiplier = (float)Momentum / 100;
        /*
        if (IsGrounded)
        {
            //movement
            if (PlayerState != PlayerState.landing)
            {
                Momentum = Vector2.zero;
                VelForce.x = MovementSpeed * DirectionalInput.x;
                if (JumpInput)
                {
                    Momentum.x = velocity.x * 0.8f;
                    VelForce.y = 10;
                    Play("JumpStart");
                    PlayerState = PlayerState.Jumping;
                }
            }
            else
            {
                PlayerState = PlayerState.Idle;
                FinalFrame = 60;
                CurrentFrame = 0;
                Animator.CrossFadeInFixedTime("IdlePlayer", 1);
            }
            
        }
        else
        {
            if (!JumpInput && velocity.y > 0) velocity.y -= 2;

            VelForce.x = MovementSpeed * DirectionalInput.x * 0.5f;
            VelForce.y = velocity.y; 
        }



        velocity = VelForce + Momentum;// +(Mathv.Clamp(VI,-Vector2.one,Vector2.right) * MovementSpeed);


        base.FixedUpdate();

        if ((VelForce.y<0)&&(!IsGrounded)) Play("AirFall"); else
        if ((PlayerState == PlayerState.Jumping) && (IsGrounded)) { Play("JumpLanding"); PlayerState = PlayerState.landing; VelForce.x = 0; }
        if (CurrentFrame < FinalFrame) CurrentFrame++; else CurrentFrame = 1;*/
        base.FixedUpdate();
    }

    /*protected void OnGUI()
    {
        
        foreach (CollisionBox box in Actioninfos["test"][0]) {
            EditorGUI.DrawRect(box + new Vector2(1280+(144*transform.position.x), 720 - (144 * transform.position.y)),Color.red);
        }
    } */
    protected void Play(string clip)
    {
        /*switch (clip)
        {
            case "JumpStart": FinalFrame = 5;break;
            case "AirFall": FinalFrame = 0;break;
            case "JumpLanding": FinalFrame = 10;break;
            case "Idle": FinalFrame = 60;break;
        }
        CurrentFrame = 0;*/
        Animator.Play(clip);
    }
}



/*
         if (Momentum > 0){
            FPSpeed = 100 / Momentum / FPDuration;
            if(IsGrounded)
            StartFP(JumpArk, new Vector2(128 * DicrectionalInput.x, 128), 30); 
        }

        if ((IsGrounded))
        {
            Momentum = 0;
            this.velocity.x = DicrectionalInput.x * MovementSpeed;
            if (JumpInput){
                Momentum = 100;
                FPSpeed = 1 / FPDuration;
            }
        }else{
                this.VI = DicrectionalInput * 0.3f;
                if (!JumpInput) {
                if (Momentum > 0)
                    Momentum -= 10;
                Debug.Log(FPMultiplier);
                    
                }
        }
*/