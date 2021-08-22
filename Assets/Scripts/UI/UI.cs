using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Level level;
    public Color winColorBackground;
    private Image bestTimeBackground;
    public Color winColorTimer;
    public Color notWonColorTimer;
    
    private GameObject indicator;
    private Color[] colors;
    private int noPlayers;

    private Image[] thumbnails = new Image[4];
    private Sprite[] pause = new Sprite[4];
    private Sprite[] play = new Sprite[4];
    private Sprite[] stop = new Sprite[4];
    private Sprite[] finished = new Sprite[4];
    private Image[] playerTimerImages = new Image[4];
    private TextMeshProUGUI[] playerTimes = new TextMeshProUGUI[4];
    private TextMeshProUGUI totalTime;
    private TextMeshProUGUI bestTimeTimer;
    public float bestTime = -1;
    
    [HideInInspector]public float maxTime;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        colors = level.timebarColors;
        maxTime = level.levelLengthInSeconds;
        noPlayers = level.players.Length;

        indicator = transform.Find("Timebar/indicator").gameObject;
        totalTime = transform.Find("Timer/TotalTime").GetComponent<TextMeshProUGUI>();
        bestTimeTimer = transform.Find("BestTimeTimer/TotalTime").GetComponent<TextMeshProUGUI>();
        bestTimeBackground = transform.Find("BestTimeTimer").GetComponent<Image>();
        UpdateBestTime();
        
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
            playerTimerImages[i] = transform.Find("PlayerUI/"+color+"Player/Timer").GetComponent<Image>();
            playerTimes[i] = transform.Find("PlayerUI/"+color+"Player/Timer/Time").GetComponent<TextMeshProUGUI>();
            playerTimerImages[i].color = new Color(1, 1, 1, 0);
            playerTimes[i].SetText("00:00");
            playerTimes[i].color = new Color(0, 0, 0, 0);
            
            // Deactivate thumbnails not needed
            if (level.players.Length == i)
            {
                thumbnails[i].color = new Color(1, 1, 1, 0);
            }
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
            cam.backgroundColor = Color.grey;
        }
        else if (player >= 0 && player < noPlayers)
        {
            cam.backgroundColor = colors[player];    
        }
        else if (player >= noPlayers)
        {
            cam.backgroundColor = winColorBackground;
        }
    }

    public void SetPlayerTime(int player, float time)
    {
        playerTimes[player].text = time.ToString("00.00");
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

    public void SetBestTime(float time)
    {
        bestTime = time;
    }
    
    public void UpdateBestTime()
    {
        if (bestTime < 0)
        {
            bestTimeTimer.text = "x";
            bestTimeBackground.color = notWonColorTimer;
        }
        else
        {
            bestTimeTimer.text = bestTime.ToString("00.00");
            bestTimeBackground.color = winColorTimer;    
        }
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
        
        playerTimerImages[player].color = new Color(1, 1, 1, 1);
        playerTimes[player].SetText("00:00");
        playerTimes[player].color = new Color(0, 0, 0, 1);
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
        
        playerTimerImages[player].color = new Color(1, 1, 1, 0);
        playerTimes[player].color = new Color(0, 0, 0, 0);
    }

    #endregion
}
