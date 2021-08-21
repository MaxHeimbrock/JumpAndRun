using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Level level;
    public Color winColor;
    
    private GameObject indicator;
    private TextMeshProUGUI totalTime;

    private Color[] colors;
    private int noPlayers;
    
    [HideInInspector]public float maxTime;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        colors = level.timebarColors;
        maxTime = level.levelLengthInSeconds;
        noPlayers = level.players.Length;

        indicator = transform.Find("Timebar/indicator").gameObject;
        totalTime = transform.Find("Timebar/Timer/TotalTime").GetComponent<TextMeshProUGUI>();
    }

    public void MoveIndicator(int frame)
    {
        indicator.transform.localPosition = new Vector3((-6 + ((frame/(maxTime*50)) * 12)), -0.011f, 0);
    }

    public void ChangePlayer(int player)
    {
        if (player == -1)
        {
            //timebar_background.color = Color.white;
            cam.backgroundColor = Color.grey;
        }
        else if (player >= 0 && player < noPlayers)
        {
            //timebar_background.color = colors[player];    
            cam.backgroundColor = colors[player];    
        }
        else if (player >= noPlayers)
        {
            //timebar_background.color = Color.grey;
            cam.backgroundColor = winColor;
        }
    }

    public void SetTotalTime(float[] times)
    {
        float result = 0;

        foreach (float time in times)
        {
            result += time;
        }
        
        totalTime.SetText(result.ToString("00.00"));
    }
}
