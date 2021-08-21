using UnityEngine;
using Image = UnityEngine.UI.Image;

public class ProgressUI : MonoBehaviour
{
    private Image[] thumbnails = new Image[4];
    private Sprite[] pause = new Sprite[4];
    private Sprite[] play = new Sprite[4];
    private Sprite[] stop = new Sprite[4];
    private Sprite[] finished = new Sprite[4];
    
    // Start is called before the first frame update
    void Start()
    {
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

            thumbnails[i] = transform.Find(color+"Player").GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
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
}
