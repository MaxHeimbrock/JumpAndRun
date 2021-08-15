using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool collected = false;
    private StateMachine stateMachine;

    private void Start()
    {
        stateMachine = FindObjectOfType<StateMachine>();
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Collected " + name);
        
        collected = true;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        
        stateMachine.CheckCollectibles();
    }

    public void Reset()
    {
        collected = false;
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }
}
