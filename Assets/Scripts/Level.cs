using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelNumber;
    public int levelLengthInSeconds = 4;
    
    public GameObject[] players;
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
