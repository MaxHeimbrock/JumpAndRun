using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public int levelLengthInSeconds = 4;
    
    public GameObject[] players;
    [HideInInspector]public int levelNumber;
    [HideInInspector]public Collider2D[] playerColliders;
    [HideInInspector]public SpriteRenderer[] playerRenderers;
    [HideInInspector] public CharacterController2D[] charControllers;
    [HideInInspector] public MoveRecorder[] moveRecorders;
    public Color[] timebarColors;

    [HideInInspector]public Collectible[] collectibles;
    [HideInInspector]public Obstacle[] obstacles;

    // Start is called before the first frame update
    void Start()
    {
        char[] levelNameCharArray = SceneManager.GetActiveScene().name.ToCharArray();
        if (levelNameCharArray.Length == 6)
        {
            levelNumber = levelNameCharArray[5]-48;    
        }
        else if (levelNameCharArray.Length == 7)
        {
            levelNumber = (levelNameCharArray[5] - 48) * 10;
            levelNumber += levelNameCharArray[6] - 48;
        }
        
        int x = (int)"5".ToCharArray()[0];
        collectibles = FindObjectsOfType<Collectible>();
        obstacles = FindObjectsOfType<Obstacle>();
        
        moveRecorders = new MoveRecorder[players.Length];
        charControllers = new CharacterController2D[players.Length];
        playerColliders = new Collider2D[players.Length];
        playerRenderers = new SpriteRenderer[players.Length];

        for (int i = 0; i < charControllers.Length; i++)
        {
            charControllers[i] = players[i].GetComponent<CharacterController2D>();
            moveRecorders[i] = players[i].GetComponent<MoveRecorder>();
            playerColliders[i] = players[i].transform.Find("CeilingCheck").GetComponent<Collider2D>();
            playerRenderers[i] = players[i].GetComponent<SpriteRenderer>();
            moveRecorders[i].SetLevelLenght(levelLengthInSeconds);
        }
    }
    
}
