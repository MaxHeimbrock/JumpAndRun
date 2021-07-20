using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float runSpeed = 40f;
    
    private float horizontalMove = 0f;
    private bool jump = false;

    private Animator animator;
    private bool hasAnimator = false;

    private MoveRecorder moveRecorder;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        {
            if (animator != null)
            {
                hasAnimator = true;
            }
        }

        /*
        moveRecorder = GetComponent<MoveRecorder>();
        moveRecorder.StartRecording(this.transform);
        */
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (hasAnimator)
        {
            animator.SetFloat("Speed", Math.Abs(horizontalMove));    
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            moveRecorder.Playback();
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    /*
    private void OnDisable()
    {
        controller.playerIsActive = false;
        moveRecorder.Playback();
    }

    private void OnEnable()
    {
        controller.playerIsActive = true;
    }
    */

    public void ChangeController(CharacterController2D controller)
    {
        controller.playerIsActive = false;
        moveRecorder.Playback();
        
        this.controller = controller;
        
        controller.playerIsActive = true;
        moveRecorder = GetComponent<MoveRecorder>();
        moveRecorder.StartRecording(this.transform);
    }
}
