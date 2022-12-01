using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum InputType
{
    noInput=-1,
    neutral=0,
    up=1,
    right=2,
    down=3,
    left=4,
    A=5,
    B=6,
    Y=7,
    X=8,
}

public class InputController : MonoBehaviour
{

    public delegate bool InputMove(int frames);
    public Dictionary<CInput[], InputMove> inputCommands = new Dictionary<CInput[], InputMove>();
    public LinkedList<CInput> inputPrompt = new LinkedList<CInput>();
    public Dictionary<CInput, float> currentInputs = new Dictionary<CInput, float>();
    public CInput lastInput;
    public CInput currentDirInput;

    bool readingInputs = false;
    int inputWindow = 4;

    CInput nextInput = InputType.neutral;
    CInput releasedCharge = InputType.neutral;

    public Vector2 DI;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            inputPrompt.AddFirst(InputType.neutral);
        }
    }

    public void AddInputCommand(CInput[] inputs, InputMove inputMove)
    {
        if (inputCommands.ContainsKey(inputs))
        {
            inputCommands[inputs] = inputCommands[inputs] + inputMove;
        }
        else
            inputCommands.Add(inputs, inputMove);
    }

    private void inputPromp(CInput input)
    {
        if (inputPrompt.Last() == InputType.neutral) inputWindow = 0;
        inputPrompt.AddLast(input);
        inputPrompt.RemoveFirst();
    }

    void FixedUpdate()
    {
        readingInputs = true;
        if (inputWindow < 60) inputWindow++;

        if (releasedCharge.window > 0) { 
            releasedCharge.window--;
            //Debug.Log("releasedCharge: " + releasedCharge);
        } else {
            releasedCharge.Neutralize();
        }

        if (!currentInputs.ContainsKey(nextInput)) nextInput = InputType.noInput;
        if (nextInput.type != InputType.noInput)
        {
            string print = "";

            inputPromp(nextInput);

            switch (nextInput.type)
            {
                case InputType.left: DI.x = -1; break;
                case InputType.right: DI.x = 1; break; 
                case InputType.up: DI.y = 1; break;
                case InputType.down: DI.y = -1; break;
                default: DI = Vector2.zero; break;
            }
            /*foreach (CInput input in inputPrompt)
            {
                print += input + " ";
            }
            Debug.Log(print);*/

            foreach (KeyValuePair<CInput[], InputMove> Move in inputCommands)
            {

                if (checkPattern(Move.Key))
                {
                    if (Move.Value(inputWindow));
                }
            }
            inputWindow = 0;
        }
        else DI = Vector2.zero;
        readingInputs = false;
        nextInput = InputType.noInput;
    }


    bool checkPattern(CInput[] pattern)
    {
        bool useCharge = false;
        int i = 0;

        if (pattern[0].Confront(releasedCharge))
        {
            i = 1;
            useCharge = true;
        }else if (pattern[0].charge < 0)
        {
            if (pattern[0].Confront(currentInputs.Last().Key))
            {
                return true;
            }
        }
       
        for (i=i; i < pattern.Length; i++)
        {
            if (!pattern[i].Confront(inputPrompt.ElementAt(inputPrompt.Count-pattern.Length+i))) return false;
        }
        if (useCharge)
        {
               releasedCharge.Neutralize();
        }
        return true;
    }

    public void RedoInput()
    {
        if (currentInputs.Count <= 0) return; readingInputs = true;
        InputType input = currentInputs.Last().Key.type;
        currentInputs.Remove(input);
        addInputInfo(input);
        readingInputs = false;
    }

    void addInputInfo(InputType input)
    {
        if (currentInputs.ContainsKey(input)) { currentInputs[input]+=Time.deltaTime*60; return; }
        if (readingInputs) return;
        currentInputs.Add(input, 0);
        nextInput = input;
    }
    void releaseInputInfo(InputType input)
    {
        if (!currentInputs.ContainsKey(input)) return;
        if(((CInput)input).preority >= releasedCharge.preority)
        if (currentInputs[input] > releasedCharge.charge)
        {
            releasedCharge = ((CInput)input).GetCharged(Mathf.FloorToInt(currentInputs[input]));
                if (currentInputs[input] > 10) ;
            //nextInput = InputType.neutral;
        }
        if (readingInputs) return;
        currentInputs.Remove(input);
    }

    // Update is called once per frame
    void Update()
    {
        int Dir = 0;
        //movement
        if (Input.GetKey(KeyCode.W)) { addInputInfo(InputType.up); Dir++; } else releaseInputInfo(InputType.up);
        if (Input.GetKey(KeyCode.D)) { addInputInfo(InputType.right); Dir++; } else releaseInputInfo(InputType.right);
        if (Input.GetKey(KeyCode.S)) { addInputInfo(InputType.down); Dir++; } else releaseInputInfo(InputType.down);
        if (Input.GetKey(KeyCode.A)) { addInputInfo(InputType.left); Dir++; } else releaseInputInfo(InputType.left);

        if (Dir == 0) addInputInfo(InputType.neutral); else releaseInputInfo(InputType.neutral);

            //attack
            if (Input.GetKey(KeyCode.Space)) addInputInfo(InputType.A); else releaseInputInfo(InputType.A);

            /*
            if (this.inputCharge == 0 && currentInputs.Count() > 0)
            {
                nextInput = currentInputs.Keys.Last();
            }*/
        
    }
    
}

public class CInput
{
    public int preority=1;
    public InputType type;
    public int window;
    public int charge;

    public CInput(InputType type,int window=8,int charge=0)
    {
        this.type = type;
        this.window = window;
        this.charge = charge;
        if (type >= InputType.A) preority++;
    }

    public void Neutralize()
    {
        if (this.type == InputType.neutral) return;
        this.type = InputType.neutral;
        this.window = 0;
        this.charge = 0;
        this.preority = 0;
    }

    public CInput GetCharged(int charge)
    {
        this.charge = charge;
        this.window = 16; 
        return this;
    }

    public bool Confront(CInput input)
    {
        return (type == input.type) && (window <= input.window) && ((this.charge == 0&&input.charge == 0) ||(this.charge!=0&&this.charge<input.charge));
    }

    public static implicit operator CInput(InputType input) => new CInput(input);
    public static bool operator ==(CInput i1,InputType i2)
    {
        return i1.type == i2;
    }
    public static bool operator !=(CInput i1, InputType i2)
    {
        return !(i1 == i2);
    }
    public static bool operator ==(CInput i1, CInput i2)
    {
        return i1.Equals(i2);
    }
    public static bool operator !=(CInput i1, CInput i2)
    {
        return !(i1 == i2);
    }
    public override bool Equals(object obj)
    {
        return obj.ToString().Equals(this.ToString());
    }
    public bool Equals(InputType t)
    {
        return (type == t);
    }
    public bool isDir()
    {
        return this.type <= InputType.A && this.type > InputType.noInput;
    }
    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }
    public override string ToString()
    {
        return type + ":" + window + ":" + charge;
    }

}
