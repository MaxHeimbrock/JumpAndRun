using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private SaveAndLoad saveAndLoad;
    private ProgressSave progress;
    private GameObject[] levelButtons;
    private Transform levelsParent;
    
    public Color colorLevelCompleted;
    public Color colorLevelNotCompleted;

    // Start is called before the first frame update
    void Start()
    {
        levelsParent = transform.Find("Levels");
        Button[] levels = levelsParent.GetComponentsInChildren<Button>();
        levelButtons = new GameObject[levels.Length];

        for (int i = 0; i < levels.Length; i++)
        {
            levelButtons[i] = levels[i].gameObject;
        }
        
        saveAndLoad = GetComponent<SaveAndLoad>();
        UpdateProgress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(int level)
    {
        //SceneManager.LoadScene("Level" + level);
    }

    public void UpdateProgress()
    {
        progress = saveAndLoad.GetProgress();

        for (int i = 0; i < levelButtons.Length; i++)
        {
            UpdateLevelButton(i);
        }
    }

    private void UpdateLevelButton(int number)
    {
        if (progress.levelSaves[number].Completed == true)
        {
            levelButtons[number].GetComponent<Image>().color = colorLevelCompleted;
            levelButtons[number].transform.Find("Time").GetComponent<TextMeshProUGUI>().text = progress.levelSaves[number].Time.ToString("00.00");
        }
        else
        {
            levelButtons[number].GetComponent<Image>().color = colorLevelNotCompleted;
            levelButtons[number].transform.Find("Time").GetComponent<TextMeshProUGUI>().text = "x";
        }
    }
}
