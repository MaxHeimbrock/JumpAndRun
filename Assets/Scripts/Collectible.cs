using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public bool collected = false;
    private StateMachine stateMachine;
    private Renderer myRenderer;
    private Collider2D myCollider2D;
    private AudioSource sound;

    private void Start()
    {
        stateMachine = FindObjectOfType<StateMachine>();
        myRenderer = GetComponent<Renderer>();
        myCollider2D = GetComponent<Collider2D>();
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D()
    {
        sound.Play();
        collected = true;
        myRenderer.enabled = false;
        myCollider2D.enabled = false;
        
        stateMachine.CollectibleCollected();
    }

    public void Reset()
    {
        collected = false;
        myRenderer.enabled = true;
        myCollider2D.enabled = true;
    }
}
