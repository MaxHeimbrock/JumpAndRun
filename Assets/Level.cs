using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelLengthInSeconds = 4;
    
    public GameObject[] players;
    [HideInInspector] public CharacterController2D[] charControllers;
    [HideInInspector] public MoveRecorder[] moveRecorders;
    public Color[] timebarColors;

    public Collectible[] collectibles;

    // Start is called before the first frame update
    void Start()
    {
        collectibles = FindObjectsOfType<Collectible>();
        
        moveRecorders = new MoveRecorder[players.Length];
        charControllers = new CharacterController2D[players.Length];
        
        for (int i = 0; i < charControllers.Length; i++)
        {
            charControllers[i] = players[i].GetComponent<CharacterController2D>();
            moveRecorders[i] = players[i].GetComponent<MoveRecorder>();
            moveRecorders[i].SetLevelLenght(levelLengthInSeconds);
        }
    }
    
}
