using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRecorder : MonoBehaviour
{
    private Queue<Vector3> recording = new Queue<Vector3>();
    private bool isRecording = false;
    private bool playback = false;
    private Transform objectToRecord;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (isRecording)
        {
            recording.Enqueue(objectToRecord.position);
        }

        if (playback && recording.Count > 0)
        {
            objectToRecord.transform.position = recording.Dequeue();
        }
    }

    public void StartRecording(Transform objectToRecord)
    {
        this.objectToRecord = objectToRecord;
        isRecording = true;
        playback = false;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void Playback()
    {
        isRecording = false;
        playback = true;
    }
}
