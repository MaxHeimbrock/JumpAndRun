using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Obstacle obstacle;
    private int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        count++;
        obstacle.MoveObstacleToEnd();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        count--;
        if (count == 0)
        {
            obstacle.MoveObstacleToStart();    
        }
    }
}

