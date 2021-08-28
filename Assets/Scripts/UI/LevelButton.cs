using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private Button button;
    private int levelNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        levelNumber = Int32.Parse(name.Split('l')[1]);
        
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => {SceneManager.LoadScene("Level" + levelNumber);});
    }
}
