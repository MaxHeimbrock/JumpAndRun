using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Sprite active;
    public Sprite passive;
    private SpriteRenderer sprite;
    
    public Obstacle obstacle;
    private int count = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        count++;
        obstacle.MoveObstacleToEnd();
        sprite.sprite = active;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        count--;
        if (count == 0)
        {
            obstacle.MoveObstacleToStart();
            sprite.sprite = passive;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);

        Vector3 startPos = transform.position;
        Vector3 endPos = obstacle.transform.position;
        
        if (!Application.isPlaying)
        {
            Gizmos.DrawLine(startPos, endPos);
        }
    }
}

