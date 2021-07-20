using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timebar : MonoBehaviour
{
    public int currentActivePlayer = 0;
    public GameObject[] players;
    private Vector3[] startingPositions;
    
    public Image timebar_background;
    public GameObject indicator;

    public Color[] colors;

    public float timer = 0;
    public float maxTime = 10.0f;

    private void Start()
    {
        startingPositions = new Vector3[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            startingPositions[i] = players[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (currentActivePlayer == players.Length + 1)
            {
                while (currentActivePlayer > 0)
                {
                    ChangePlayer(-1);    
                }
            }

            ChangePlayer(+1);
            
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (currentActivePlayer != 0)
            {
                ChangePlayer(-1);
            }
        }

        if (currentActivePlayer != 0 && currentActivePlayer != players.Length + 1)
        {
            if (timer <= maxTime)
            {
                timer += Time.deltaTime;
                MoveIndicator(timer);
            }
            else
            {
                ChangePlayer(+1);
            }
        }
    }


    private void MoveIndicator(float seconds)
    {
        indicator.transform.localPosition = new Vector3((-6 + ((seconds/maxTime) * 12)), -0.011f, 0);
    }

    private void ChangePlayer(int change)
    {
        //Deactivate
        for (int i = 0; i < currentActivePlayer + 1; i++)
        {
            EnablePlayer(i, false);    
        }
        
        // Increment / Decrement
        currentActivePlayer = (currentActivePlayer + change);
        // Activate
        EnablePlayer(currentActivePlayer, true);
        
        if (currentActivePlayer == players.Length + 1)
        {
            timebar_background.color = colors[colors.Length-1];
        }
        else
        {
            timebar_background.color = colors[currentActivePlayer];    
        }

        timer = 0.0f;
        
        if (currentActivePlayer == 0 || currentActivePlayer == players.Length + 1)
        {
            MoveIndicator(timer);
        }
    }

    private void EnablePlayer(int playerNumber, bool enabled)
    {
        
        if (playerNumber == 0 || playerNumber > players.Length)
        {
            // Not a player index
            return;
        }
        else
        {
            players[playerNumber - 1].transform.position = startingPositions[playerNumber - 1];
            players[playerNumber-1].GetComponent<PlayerMovement>().enabled = enabled;
        }
    }
}
