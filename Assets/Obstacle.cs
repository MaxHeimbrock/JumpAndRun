using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
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
        endPosition = transform.Find("MoveToEnd").position;
    }

    // Update is called once per frame
    void Update()
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
}
