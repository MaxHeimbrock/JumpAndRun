using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRecorder : MonoBehaviour
{
    public int levelLengthInFrames;
    
    private Vector3 initialPos;
    private List<Vector3> recording = new List<Vector3>();
    private bool isRecording = false;
    private bool playback = false;
    private Transform objectToRecord;
    private int playbackFrameIndex = 0;
    
    public delegate void LevelTimeUpCallback();
    public LevelTimeUpCallback callback;
    
    public void SetLevelLenght(int levelLengthInSeconds)
    {
        levelLengthInFrames = levelLengthInSeconds * 50;
    }

    void FixedUpdate()
    {
        if (isRecording && recording.Count <= levelLengthInFrames)
        {
            recording.Add(objectToRecord.position);
        }
        else if (isRecording && recording.Count > levelLengthInFrames)
        {
            isRecording = false;
            callback();
        }

        if (playback && recording.Count > 0)
        {
            objectToRecord.transform.position = recording[playbackFrameIndex];
            playbackFrameIndex++;
            
            if (playbackFrameIndex == recording.Count)
            {
                playbackFrameIndex = 0;
            }
        }
    }

    public void ResetRecording()
    {
        playbackFrameIndex = 0;
        isRecording = false;
        playback = false;
        recording = new List<Vector3>();
        objectToRecord.transform.position = initialPos;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void StartRecording(Transform objectToRecord, LevelTimeUpCallback callback)
    {
        this.callback = callback;
        this.objectToRecord = objectToRecord;
        initialPos = objectToRecord.position;
        ResetRecording();
        isRecording = true;
    }

    public void Playback()
    {
        playbackFrameIndex = 0;
        isRecording = false;
        playback = true;
    }

    public void ResetPlayback()
    {
        playbackFrameIndex = 0;
    }
}
