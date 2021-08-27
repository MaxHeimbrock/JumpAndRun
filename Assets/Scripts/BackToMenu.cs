using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
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
}