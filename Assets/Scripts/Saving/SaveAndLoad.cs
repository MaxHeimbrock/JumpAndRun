using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SaveAndLoad : MonoBehaviour
{
    private string SAVE_FOLDER;
    public string mySaveName = "Max";
    private string mySaveFilePath;
    private ProgressSave saveFile;

    private void Awake()
    {
        SAVE_FOLDER = Application.dataPath + "/Saves/";
        
        mySaveFilePath = SAVE_FOLDER + mySaveName + ".json";
        
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER); 
        }
        if (!File.Exists(mySaveFilePath))
        {
            CreateNewSaveFile();
        }
        else
        {
            LoadSaveFile();
        }
    }

    public void LoadSaveFile()
    {
        string saveFileJson = System.IO.File.ReadAllText(mySaveFilePath);
        saveFile = JsonUtility.FromJson<ProgressSave>(saveFileJson);
    }

    public void CreateNewSaveFile()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Menu"))
        {
            Debug.Log("Savefile can only be set up in menu");
            return;
        }

        int numberOfLevels = transform.Find("Levels").childCount;
        saveFile = new ProgressSave();

        for (int i = 0; i < numberOfLevels; i++)
        {
            saveFile.AddLevelSave(new LevelSave(i+1, false, -1));    
        }
        
        RewriteSaveFile();
    }

    void RewriteSaveFile()
    {
        string saveFileAsJson = JsonUtility.ToJson(saveFile);
        System.IO.File.WriteAllText(mySaveFilePath, saveFileAsJson);
    }
    
    public void SaveLevelWon(int levelNumber, float time)
    {
        saveFile.SetLevelSave(levelNumber, time);
        RewriteSaveFile();
    }

    public ProgressSave GetProgress()
    {
        return saveFile;
    }
}
