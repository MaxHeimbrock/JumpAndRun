using System.Collections;
using System.Collections.Generic;
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
        char[] levelNameCharArray = name.ToCharArray();
        if (levelNameCharArray.Length == 6)
        {
            levelNumber = levelNameCharArray[5]-48;    
        }
        else if (levelNameCharArray.Length == 7)
        {
            levelNumber = (levelNameCharArray[5] - 48) * 10;
            levelNumber += levelNameCharArray[6] - 48;
        }
        
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => {SceneManager.LoadScene("Level" + levelNumber);});
    }
}
