using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundState : MonoBehaviour
{
    public int turn = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndTurn()
    {
        turn = (turn + 1) % 2;
    }
}
