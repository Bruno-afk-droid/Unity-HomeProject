using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Condition
{
    neutral=0,
    attack=1,
    airborn=2,
}

public abstract class State
{
    protected float time { get; set; }
    protected float fixedTime { get; set; }
    protected float lateTime { get; set; }

    public StateMachine stateMachine;
    public touchable Owner { get { return stateMachine.Owner; } }
    public State BufferdState { get { return stateMachine.bufferedState; } set { stateMachine.bufferedState = value;} }
    public State ReturnState;
    public Condition Condition = Condition.neutral;
    public Vector4 velocity;
    public Vector2 DI_vel;
    public bool isActive;
    // Called at the beginning to set intial variables
    public virtual void OnEnter(StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
        stateMachine.Owner.velocity = GetAppliedVelocity();
        isActive = true;
    }

    public abstract void setup();

    public abstract bool Buffer(int frames);
    public Vector2 GetAppliedVelocity()
    {
        return new Vector2((Owner.velocity.x*(1-velocity.z))+(velocity.x*(velocity.z)), (Owner.velocity.y * (1 - velocity.w)) + (velocity.y * (velocity.w)));
    }

    public void ApplyDI(Vector2 DI)
    {
        DI *= this.DI_vel;
        Owner.velocity.x += DI.x;
        Owner.velocity.y += DI.y;
    }

    // Called every frame
    public virtual void OnUpdate()
    {
        time += Time.deltaTime;
    }
    public virtual void OnFixedUpdate()
    {
        fixedTime += Time.deltaTime;
    }
    public virtual void OnLateUpdate()
    {
        lateTime += Time.deltaTime;
    }
    // Called at the end to clean up any data
    public virtual void OnExit()
    {
        isActive = false;
        time = 0;
        fixedTime = 0;
        lateTime = 0;
    }

    
    #region Passthrough Methods
    protected static void Destroy(UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }

    protected T GetComponent<T>() where T : Component { return stateMachine.GetComponent<T>(); }
    protected Component GetComponent(System.Type type) { return stateMachine.GetComponent(type); }
    protected Component GetComponent(string type) { return stateMachine.GetComponent(type); }
    #endregion
}

