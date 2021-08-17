using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRecorder : MonoBehaviour
{
    public int levelLengthInFrames;
    
    private Vector3 initialPos;
    private Vector3[] recording;
    private Transform objectToRecord;

    public void SetLevelLenght(int levelLengthInSeconds)
    {
        levelLengthInFrames = levelLengthInSeconds * 50;
        recording = new Vector3[levelLengthInFrames];
    }

    public void PlaybackFrame(int frame)
    {
        try
        {
            objectToRecord.transform.position = recording[frame];
        }
        catch (Exception e)
        {
            Debug.LogWarning("error in frame " + frame + "\n" + e);
        }
        
    }

    public void RecordFrame(int frame)
    {
        recording[frame] = objectToRecord.position;
    }

    public void ResetRecording()
    {
        recording = new Vector3[levelLengthInFrames];
        objectToRecord.transform.position = initialPos;
    }

    public void StartRecording(Transform objectToRecord)
    {
        this.objectToRecord = objectToRecord;
        initialPos = objectToRecord.position;
        ResetRecording();
    }
    public void ResetPlayback()
    {
        objectToRecord.position = initialPos;
    }
}
