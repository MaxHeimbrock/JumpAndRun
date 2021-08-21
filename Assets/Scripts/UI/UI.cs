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
    
    
    private Image[] thumbnails = new Image[4];
    private Sprite[] pause = new Sprite[4];
    private Sprite[] play = new Sprite[4];
    private Sprite[] stop = new Sprite[4];
    private Sprite[] finished = new Sprite[4];
    
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
        
        string color = "";

        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    color = "blue";
                    break;
                case 1:
                    color = "red";
                    break;
                case 2:
                    color = "green";
                    break;
                case 3:
                    color = "pink";
                    break;
            }
            pause[i] = Resources.Load<Sprite>("UI/" + color + "_pause");
            play[i] = Resources.Load<Sprite>("UI/" + color + "_play");
            stop[i] = Resources.Load<Sprite>("UI/" + color + "_stop");
            finished[i] = Resources.Load<Sprite>("UI/" + color + "_finished");

            thumbnails[i] = transform.Find("PlayerUI/"+color+"Player").GetComponent<Image>();
        }
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

    #region PlayerThumbnails

    public void SetPlayerFinished(int player)
    {
        thumbnails[player].sprite = finished[player];
    }

    public void SetPlayerPause(int player)
    {
        thumbnails[player].sprite = pause[player];
        thumbnails[player].color = new Color(1, 1, 1, 1);
    }
    
    public void SetPlayerPlay(int player)
    {
        thumbnails[player].sprite = play[player];
    }
    
    public void SetPlayerStop(int player)
    {
        thumbnails[player].sprite = stop[player];
    }
    
    public void SetPlayerTransparent(int player)
    {
        thumbnails[player].sprite = finished[player];
        thumbnails[player].color = new Color(1, 1, 1, 0.5f);
    }

    #endregion
}
