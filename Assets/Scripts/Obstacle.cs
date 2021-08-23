using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Transform endPositionGameObject;
    
    private Vector3 startPosition;
    private Vector3 endPosition;

    private enum ObstacleState {Start, End}
    private ObstacleState currentlyGoingTo = ObstacleState.Start;
    private float timer = 0f;
    public float timeToMove = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        endPosition = endPositionGameObject.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentlyGoingTo == ObstacleState.End)
        {
            if (timer < timeToMove)
            {
                timer += Time.deltaTime;    
            }
        }
        else
        {
            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;    
            }
        }
        
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, timer/timeToMove);
    }

    public void MoveObstacleToEnd()
    {
        currentlyGoingTo = ObstacleState.End;
    }

    public void MoveObstacleToStart()
    {
        currentlyGoingTo = ObstacleState.Start;
    }

    public void Reset()
    {
        timer = 0;
        transform.position = startPosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Vector3 startPos;
        Vector3 endPos;
        if (Application.isPlaying)
        {
            startPos = startPosition;
            endPos = endPosition;
            if (timer > 0)
            {
                Gizmos.DrawCube(startPos, new Vector3(1, 1, 1));    
            }
        }
        else
        {
            startPos = transform.position;
            endPos = endPositionGameObject.position;
        }
        
        Gizmos.DrawCube(endPos, new Vector3(1, 1, 1));
        Gizmos.DrawLine(startPos, endPos);
    }
}
