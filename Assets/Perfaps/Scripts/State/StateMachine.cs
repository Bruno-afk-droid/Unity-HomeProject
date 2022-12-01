using UnityEngine;
public class StateMachine : MonoBehaviour
{
    public InputController inputController { get; private set; }
    public Animator Animator { get; private set; }
    public MeleeHitBox MeleeHitBox { get; private set; }
    public touchable Owner { get; private set; }
    private State MainState;
    public State CurrentState { get; private set; }
    private State nextState;
    public State bufferedState;
    public Condition Condition { get { return CurrentState.Condition; } }

    public void Awake()
    {
        Animator = this.GetComponent<Animator>();
        MeleeHitBox = this.GetComponentInChildren<MeleeHitBox>();
        Owner = this.GetComponent<touchable>();
        MainState = new MainIdleState();
        SetState(MainState);
    }
    public void setup()
    {
        this.inputController = GetComponent<InputController>();
        SetState(MainState);
        MainState.setup();
    }

    private void Update()
    {
        if (nextState != null)
        {
            SetState(nextState);
            nextState = null;
        }
        if (CurrentState != null)
            CurrentState.OnUpdate();
    }

    private void SetState(State _newState)
    {
        if (CurrentState != null)
        {
            CurrentState.OnExit();
        }
        CurrentState = _newState;
        CurrentState.OnEnter(this);
    }

    public void SetNextState(State _newState)
    {
        if (_newState != null)
        {
            nextState = _newState;
        }
    }

    public void SetNextStateToMain()
    {
        SetNextState(MainState);
    }

    public void SetStateToMain()
    {
        SetNextState(MainState);
    }

    public void GoToNextState()
    {
        if (bufferedState != null)
        {
            SetState(bufferedState);
            bufferedState = null;
        }
        else
        {
            SetStateToMain();
        }
    }

    private void LateUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.OnLateUpdate();
        }
    }

    private void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.OnFixedUpdate();
        }
    }
}
