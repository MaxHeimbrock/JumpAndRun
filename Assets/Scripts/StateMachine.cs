using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class StateMachine : MonoBehaviour
{
    public string stateName;
    public Transform playerIndicator;
    private Level levelInfo;
    public float[] playerTimes;
    public UI ui;
    private SaveAndLoad saveAndLoad;
    private AudioSource soundOutput;
    
    protected State state;
    private int currentPlayer = -1;

    public void Start()
    {
        levelInfo = GetComponent<Level>();
        playerTimes = new float[levelInfo.players.Length];
        state = new Neutral(this);
        state.Enter();
        saveAndLoad = GetComponent<SaveAndLoad>();
        soundOutput = GetComponent<AudioSource>();
        ui.SetBestTime(saveAndLoad.GetProgress().levelSaves[levelInfo.levelNumber-1].Time);
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
    
    public void RestartAllPlaybacks()
    {
        for (int i = 0; i < currentPlayer; i++)
        {
            levelInfo.moveRecorders[i].ResetPlayback();
        }
    }

    public void PlaybackAllBefore(int frame)
    {
        if (frame == 0)
        {
            ResetCollectibles();
        }

        for (int i = 0; i < currentPlayer; i++)
        {
            levelInfo.moveRecorders[i].PlaybackFrame(frame);
        }
    }
    
    public void PlaybackAllAndCurrent(int frame)
    {
        if (frame == 0)
        {
            ResetCollectibles();
        }
        
        for (int i = 0; i < currentPlayer+1; i++)
        {
            levelInfo.moveRecorders[i].PlaybackFrame(frame);
        }
    }

    public void ResetCollectibles()
    {
        foreach (var collectible in levelInfo.collectibles)
        {
            collectible.Reset();
        } 
    }

    public void ResetAllPlayback()
    {
        for (int i = 0; i < currentPlayer; i++)
        {
            levelInfo.moveRecorders[i].ResetPlayback();
        }
    }

    public void CollectibleCollected()
    {
        if (state.GetType() == typeof(End))
        {
            return;
        }
        
        bool won = true;
        
        foreach (Collectible collectible in levelInfo.collectibles)
        {
            won = won && collectible.collected;
        }

        if (won)
        {
            state.FixedUpdate(); // Save winning pos as well
            
            levelInfo.charControllers[currentPlayer].playerIsActive = false;
            ui.SetPlayerFinished(currentPlayer);
            playerTimes[currentPlayer] = ((float)state.frame / 50);
            ui.SetPlayerTime(currentPlayer, ((float)state.frame / 50));
            ui.SetTotalTime(playerTimes);
            
            currentPlayer++;

            playerIndicator.gameObject.GetComponent<Renderer>().enabled = false;
            ui.ChangePlayer(levelInfo.players.Length);
            
            SetState(new End(this, state.frame));
            
            float totalTime = 0;
            float oldTime = saveAndLoad.GetProgress().levelSaves[levelInfo.levelNumber-1].Time;
        
            foreach (float time in playerTimes)
            {
                totalTime += time;
            }
        
            if (oldTime > totalTime || oldTime < 0) // -1 is default if not won already
            {
                soundOutput.Play();
                saveAndLoad.SaveLevelWon(levelInfo.levelNumber, totalTime);
                ui.SetBestTime(totalTime);
                ui.UpdateBestTime();
            }
        }
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
            
            stateMachine.playerIndicator.SetParent(stateMachine.levelInfo.players[stateMachine.currentPlayer].transform);
            stateMachine.ui.ChangePlayer(stateMachine.currentPlayer);
            stateMachine.ui.SetPlayerPause(stateMachine.currentPlayer);
            stateMachine.levelInfo.playerColliders[stateMachine.currentPlayer].isTrigger = false;
            stateMachine.levelInfo.playerRenderers[stateMachine.currentPlayer].color = new Color(1, 1, 1, 1);
            stateMachine.playerIndicator.gameObject.GetComponent<Renderer>().enabled = true;
            
            stateMachine.SetState(new Ready(stateMachine));
        }
    }
    
    protected class Ready : State
    {
        private float horizontalMove;
        private bool jump;
        
        public Ready(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.ResetAllPlayback();
            stateMachine.ResetCollectibles();
            stateMachine.ui.MoveIndicator(0);
        }

        public override void Update()
        {
            // Backward
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GoToPreviousState();
            }

            horizontalMove = Input.GetAxisRaw("Horizontal");
            jump = Input.GetButtonDown("Jump");
            
            if (horizontalMove != 0 || jump)
            {
                GoToNextState();
            }
        }

        public override void GoToNextState()
        {
            stateMachine.SetState(new Playing(stateMachine, horizontalMove, jump));
            stateMachine.ui.SetPlayerPlay(stateMachine.currentPlayer);
        }
        
        public override void GoToPreviousState()
        {
            stateMachine.ui.SetPlayerTransparent(stateMachine.currentPlayer);
            stateMachine.levelInfo.playerColliders[stateMachine.currentPlayer].isTrigger = true;
            stateMachine.levelInfo.playerRenderers[stateMachine.currentPlayer].color = new Color(1, 1, 1, 0.5f);
            stateMachine.playerTimes[stateMachine.currentPlayer] = 0;
            stateMachine.ui.SetTotalTime(stateMachine.playerTimes);
            
            stateMachine.currentPlayer--;
            
            stateMachine.ui.ChangePlayer(stateMachine.currentPlayer);
            if (stateMachine.currentPlayer == -1)
            {
                stateMachine.playerIndicator.gameObject.GetComponent<Renderer>().enabled = false;
                
                stateMachine.SetState(new Neutral(stateMachine));
            }
            else
            {
                stateMachine.ui.SetPlayerStop(stateMachine.currentPlayer);
                stateMachine.playerIndicator.SetParent(stateMachine.levelInfo.players[stateMachine.currentPlayer].transform, false);
                
                stateMachine.SetState(new Review(stateMachine));
            }
        }
    }
    
    protected class Playing : State
    {
        private CharacterController2D controller;
        private MoveRecorder moveRecorder;
        private List<Collectible> collectiblesCollected;
        
        public float runSpeed = 40f;
        private float horizontalMove = 0f;
        private bool jump = false;

        public Playing(StateMachine stateMachine, float horizontalMove, bool jump) : base(stateMachine)
        {
            this.horizontalMove = horizontalMove;
            this.jump = jump;
        }

        public override void Enter()
        {
            controller = stateMachine.levelInfo.charControllers[stateMachine.currentPlayer];
            controller.playerIsActive = true;
            moveRecorder = stateMachine.levelInfo.moveRecorders[stateMachine.currentPlayer];
            moveRecorder.StartRecording(controller.transform);

            stateMachine.RestartAllPlaybacks();
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

            stateMachine.ui.SetPlayerTime(stateMachine.currentPlayer, ((float)frame / 50));
        }
        
        public override void FixedUpdate()
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
            
            stateMachine.PlaybackAllBefore(frame);
            moveRecorder.RecordFrame(frame);
            frame++;
            stateMachine.ui.MoveIndicator(frame);

            if (frame >= stateMachine.levelInfo.levelLengthInSeconds * 50)
            {
                GoToNextState();
            }
        }

        public override void GoToPreviousState()
        {
            controller.playerIsActive = false;
            moveRecorder.ResetRecording();
            stateMachine.RestartAllPlaybacks();
            stateMachine.ui.SetPlayerPause(stateMachine.currentPlayer);
            stateMachine.playerTimes[stateMachine.currentPlayer] = 0;
            stateMachine.ui.SetTotalTime(stateMachine.playerTimes);
            
            stateMachine.SetState(new Ready(stateMachine));
        }
        
        public override void GoToNextState()
        {
            controller.playerIsActive = false;
            stateMachine.playerTimes[stateMachine.currentPlayer] = ((float) frame / 50);
            stateMachine.ui.SetPlayerStop(stateMachine.currentPlayer);
            stateMachine.ui.SetTotalTime(stateMachine.playerTimes);
            stateMachine.ui.SetPlayerTime(stateMachine.currentPlayer, ((float)frame / 50));
            
            stateMachine.SetState(new Review(stateMachine));
        }
    }
    
    protected class Review : State
    {
        public Review(StateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void FixedUpdate()
        {
            stateMachine.PlaybackAllAndCurrent(frame);
            frame++;
            if (frame >= stateMachine.levelInfo.levelLengthInSeconds * 50)
            {
                frame = 0;
            }
            stateMachine.ui.MoveIndicator(frame);
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
                if (stateMachine.currentPlayer < stateMachine.levelInfo.players.Length - 1)
                {
                    GoToNextState();    
                }
            }
        }

        public override void GoToPreviousState()
        {
            stateMachine.levelInfo.moveRecorders[stateMachine.currentPlayer].ResetRecording();
            stateMachine.ui.SetPlayerPause(stateMachine.currentPlayer);
            stateMachine.playerTimes[stateMachine.currentPlayer] = 0;
            stateMachine.ui.SetTotalTime(stateMachine.playerTimes);
            
            stateMachine.SetState(new Ready(stateMachine));
        }

        public override void GoToNextState()
        {
            stateMachine.ui.SetPlayerFinished(stateMachine.currentPlayer);
            
            stateMachine.currentPlayer++;
            
            stateMachine.playerIndicator.SetParent(stateMachine.levelInfo.players[stateMachine.currentPlayer].transform, false);
            stateMachine.ui.ChangePlayer(stateMachine.currentPlayer);
            stateMachine.ui.SetPlayerPause(stateMachine.currentPlayer);
            stateMachine.levelInfo.playerColliders[stateMachine.currentPlayer].isTrigger = false;
            stateMachine.levelInfo.playerRenderers[stateMachine.currentPlayer].color = new Color(1, 1, 1, 1);
            
            stateMachine.SetState(new Ready(stateMachine));
        }
    }

    protected class End : State
    {
        private int finalFrame;
        
        public End(StateMachine stateMachine, int finalFrame) : base(stateMachine)
        {
            this.finalFrame = finalFrame;
        }

        public override void Update()
        {
            // Backward
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GoToPreviousState();
            }
        }

        public override void FixedUpdate()
        {
            stateMachine.PlaybackAllBefore(frame);
            frame++;
            if (frame >= stateMachine.levelInfo.levelLengthInSeconds * 50)
            {
                frame = 0;
            }
            stateMachine.ui.MoveIndicator(frame);
        }
        
        public override void GoToPreviousState()
        {
            stateMachine.currentPlayer--;
            stateMachine.playerIndicator.gameObject.GetComponent<Renderer>().enabled = true;
            stateMachine.playerIndicator.SetParent(stateMachine.levelInfo.players[stateMachine.currentPlayer].transform, false);
            stateMachine.ui.ChangePlayer(stateMachine.currentPlayer);
            stateMachine.ui.SetPlayerPause(stateMachine.currentPlayer);
            stateMachine.levelInfo.moveRecorders[stateMachine.currentPlayer].ResetRecording();
            stateMachine.playerTimes[stateMachine.currentPlayer] = 0;
            stateMachine.ui.SetTotalTime(stateMachine.playerTimes);
            
            stateMachine.SetState(new Ready(stateMachine));
        }
    }

    #endregion
}

public abstract class State
{
    protected StateMachine stateMachine;
    public int frame = 0;
        
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