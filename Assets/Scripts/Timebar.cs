using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timebar : MonoBehaviour
{
    public Level level;
    
    public Image timebar_background;
    public GameObject indicator;

    private Color[] colors;
    private int noPlayers;
    
    public float maxTime;
    public float timer = 0;

    private void Start()
    {
        colors = level.timebarColors;
        maxTime = level.levelLengthInSeconds;
        noPlayers = level.players.Length;
    }

    public void MoveIndicator(int frame)
    {
        indicator.transform.localPosition = new Vector3((-6 + ((frame/(maxTime*50)) * 12)), -0.011f, 0);
    }

    public void ChangePlayer(int player)
    {
        if (player == -1)
        {
            timebar_background.color = Color.white;
        }
        else if (player >= 0 && player < noPlayers)
        {
            timebar_background.color = colors[player];    
        }
        else if (player >= noPlayers)
        {
            timebar_background.color = Color.grey;   
        }
    }
}
