using UnityEngine;

public class ShowTutorial : MonoBehaviour
{
    public GameObject helper_indicator;
    public GameObject[] tutorial_images;
    public GameObject[] ui_blocks;
    private UI_State state;
    
    // Start is called before the first frame update
    void Start()
    {
        SetState(new UI_State(this, 0), -1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        state.Update();
    }

    public void SetState(UI_State state, int from, int to)
    {
        if (from != -1)
        {
            ChangeUI(from, to);
        }
            
        this.state = state;
    }

    public void ChangeUI(int from, int to)
    {
        tutorial_images[from].SetActive(false);
        
        helper_indicator.transform.SetParent(ui_blocks[to].transform, false);
        tutorial_images[to].SetActive(true);
    }
}

public class UI_State
{
    protected ShowTutorial showTutorial;
    protected int number;
        
    public UI_State(ShowTutorial showTutorial, int number)
    {
        this.showTutorial = showTutorial;
        this.number = number;
    }
    
    public void Update()
    {
        // Backward
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (number == 0)
            {
                showTutorial.gameObject.SetActive(false);
            }
            else
            {
                GoToPreviousState();    
            }
        }
            
        // Forward
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (number == 4)
            {
                showTutorial.gameObject.SetActive(false);
            }
            else
            {
                GoToNextState();    
            }
        }
    }
        
    public void GoToNextState()
    {
        showTutorial.SetState(new UI_State(showTutorial, number+1), number, number+1);
    }
    
    public void GoToPreviousState()
    {
        showTutorial.SetState(new UI_State(showTutorial, number-1), number, number-1);
    }
}
