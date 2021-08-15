using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public string stateName;

    private Level levelInfo;
    protected State state;
    private int currentPlayer = -1;
    private PlayerMovement playerMovement;

    public void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        levelInfo = GetComponent<Level>();
        
        state = new Neutral(this);
        state.Enter();
    }

    public void Update()
    {
        stateName = UpdateStateName();
        
        state.Update();
    }

    private String UpdateStateName()
    {
        String result = "";
        
        if (state.GetType() == typeof(Neutral))
        {
            result += "Neutral";
        }
        else if (state.GetType() == typeof(Ready))
        {
            result += "Ready";
        }
        else if (state.GetType() == typeof(Playing))
        {
            result += "Playing";
        }
        else if (state.GetType() == typeof(Review))
        {
            result += "Review";
        }
        else if (state.GetType() == typeof(End))
        {
            result += "End";
        }

        result += " ";
        result += currentPlayer;

        return result;
    }

    public void FixedUpdate()
    {
        state.FixedUpdate();
    }

    public void SetState(State state)
    {
        this.state = state;
        state.Enter();
    }

    #region States

    protected class Neutral : State
    {
        public Neutral(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Update()
        {
            // Forward
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GoToNextState();
            }
        }

        public override void GoToNextState()
        {
            stateMachine.currentPlayer++;
            stateMachine.SetState(new Ready(stateMachine));
        }
    }
    
    protected class Ready : State
    {
        public Ready(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Update()
        {
            // Backward
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GoToPreviousState();
            }
            
            if (Input.GetAxisRaw("Horizontal") != 0 || (Input.GetButtonDown("Jump")))
            {
                GoToNextState();
            }
        }

        public override void GoToNextState()
        {
            stateMachine.SetState(new Playing(stateMachine));
        }
        
        public override void GoToPreviousState()
        {
            stateMachine.currentPlayer--;
            if (stateMachine.currentPlayer == -1)
            {
                stateMachine.SetState(new Neutral(stateMachine));
            }
            else
            {
                stateMachine.SetState(new Review(stateMachine));
            }
        }
    }
    
    protected class Playing : State
    {
        private CharacterController2D controller;
        private MoveRecorder moveRecorder;

        private MoveRecorder.LevelTimeUpCallback TimeUpCallback;
        
        public float runSpeed = 40f;
        private float horizontalMove = 0f;
        private bool jump = false;

        public void Test()
        {
            
        }
        
        public Playing(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            controller = stateMachine.levelInfo.charControllers[stateMachine.currentPlayer];
            controller.playerIsActive = true;
            moveRecorder = stateMachine.levelInfo.moveRecorders[stateMachine.currentPlayer];
            moveRecorder.StartRecording(controller.transform, () => {GoToNextState();});
        }

        public override void Update()
        {
            // Backward
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GoToPreviousState();
            }
            
            // Forward
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GoToNextState();
            }
            
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
        }
        
        public override void FixedUpdate()
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }

        public override void GoToPreviousState()
        {
            controller.playerIsActive = false;
            moveRecorder.ResetRecording();
            stateMachine.SetState(new Ready(stateMachine));
        }
        
        public override void GoToNextState()
        {
            controller.playerIsActive = false;
            stateMachine.SetState(new Review(stateMachine));
            moveRecorder.StopRecording();
            moveRecorder.Playback();
        }
    }
    
    protected class Review : State
    {
        public Review(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Update()
        {
            // Backward
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GoToPreviousState();
            }
            
            // Forward
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GoToNextState();
            }
        }

        public override void GoToPreviousState()
        {
            stateMachine.SetState(new Ready(stateMachine));
            stateMachine.levelInfo.moveRecorders[stateMachine.currentPlayer].ResetRecording();
        }

        public override void GoToNextState()
        {
            stateMachine.currentPlayer++;
            if (stateMachine.currentPlayer == stateMachine.levelInfo.players.Length)
            {
                stateMachine.SetState(new End(stateMachine));
            }
            else
            {
                stateMachine.SetState(new Ready(stateMachine));
            }
        }
    }

    protected class End : State
    {
        public End(StateMachine stateMachine) : base(stateMachine)
        {
            Debug.Log("End");
        }
    }

    #endregion
}

public abstract class State
{
    protected StateMachine stateMachine;
        
    public State(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
    }
    
    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }
        
    public virtual void GoToNextState()
    {
    }
    
    public virtual void GoToPreviousState()
    {
    }
}