using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRecorder : MonoBehaviour
{
    public int levelLengthInFrames;
    
    private Vector3 initialPos;
    private Vector3[] recording;
    private bool isRecording = false;
    private bool playback = false;
    private Transform objectToRecord;
    private int playbackFrameIndex = 0;

    public void SetLevelLenght(int levelLengthInSeconds)
    {
        levelLengthInFrames = levelLengthInSeconds * 50;
        recording = new Vector3[levelLengthInFrames];
    }

    public void PlaybackFrame(int frame)
    {
        objectToRecord.transform.position = recording[frame];
    }

    public void RecordFrame(int frame)
    {
        recording[frame] = objectToRecord.position;
    }

    public void ResetRecording()
    {
        playbackFrameIndex = 0;
        isRecording = false;
        playback = false;
        recording = new Vector3[levelLengthInFrames];
        objectToRecord.transform.position = initialPos;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void StartRecording(Transform objectToRecord)
    {
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
