using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
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
            SetUpProgressFile();
        }
        else
        {
            ReadProgressFile();
        }
    }

    private void ReadProgressFile()
    {
        string saveFileJson = System.IO.File.ReadAllText(mySaveFilePath);
        saveFile = JsonUtility.FromJson<ProgressSave>(saveFileJson);
    }

    void SetUpProgressFile()
    {
        saveFile = new ProgressSave();
        saveFile.AddLevelSave(new LevelSave(1, false, -1));
        saveFile.AddLevelSave(new LevelSave(2, false, -1));
        saveFile.AddLevelSave(new LevelSave(3, false, -1));
    
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
