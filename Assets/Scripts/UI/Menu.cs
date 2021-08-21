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
    public GameObject[] levelButtons;

    public Color colorLevelCompleted;
    public Color colorLevelNotCompleted;
    
    // Start is called before the first frame update
    void Start()
    {
        saveAndLoad = GetComponent<SaveAndLoad>();
        UpdateProgress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
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
    }
}
