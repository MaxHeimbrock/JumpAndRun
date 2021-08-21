using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveAndLoad : MonoBehaviour
{
    private static string SAVE_FOLDER;
    public string mySaveName = "Max";
    private string mySaveFilePath;
    private ProgressSave saveFile;
    
    
    private void Awake()
    {
        SAVE_FOLDER = Application.dataPath + "/Saves/";
    }

    // Start is called before the first frame update
    void Start()
    {
        mySaveFilePath = SAVE_FOLDER + mySaveName + ".json";
        
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER); 
        }
        if (!File.Exists(mySaveFilePath))
        {
            SetUpProgressFile(mySaveFilePath);
        }
        
        SetUpProgressFile(mySaveFilePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpProgressFile(string filePath)
    {
        saveFile = new ProgressSave();
        saveFile.AddLevelSave(new LevelSave(1, false, -1));
        saveFile.AddLevelSave(new LevelSave(2, false, -1));
        saveFile.AddLevelSave(new LevelSave(3, false, -1));

        string saveFileAsJson = JsonUtility.ToJson(saveFile);
        System.IO.File.WriteAllText(filePath, saveFileAsJson);
        Debug.Log("Created new ProgressFile for " + mySaveName + "\nIn path " + filePath);
    }
}
