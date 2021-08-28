using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomButton : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToMenu();
        }
    }
    
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToNextLevel()
    {
        int levelNumber = Int32.Parse(SceneManager.GetActiveScene().name.Split('l')[1]) + 1;
        string nextLevelSceneName = "Level" + levelNumber;
        if (levelNumber > Level.NUMBER_OF_LEVELS)
        {
            nextLevelSceneName = "Menu";
        }
        SceneManager.LoadScene(nextLevelSceneName);
    }
}
