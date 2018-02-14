using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    //GameManager is used to hold the PG of the map and create a new map if need be.

    public Maze mazePrefab;
    public Player playerPrefab;
    public UsableItem BatteriesPreFab;
    public GhostAi GhostPreFab;
    public static int numbBatteries = 10;


    public static Maze mazeInstance;
    private Player playerInstance;
    private UsableItem BatteriesInstance;
    private GhostAi GhostInstance;
    private bool itemPlaced = false;
    private bool ghostPlaced = false;
    public static bool win = false;
    public static bool dead = false;




    void Start () {
        //StartCoroutine(BeginGame());
        BeginGame();
    }
	
	void Update () {
	    if(numbBatteries <= 0)
        {
            win = true;
        }
        else if(Battery_Life.bLife <= 0.0f || Player.health <= 0.0f)
        {
            dead = true;
        }
            
	}

    //function that is called that instantiates all of the entities in the game
    private void BeginGame ()
    {
        mazeInstance = Instantiate(mazePrefab) as Maze;
        //yield return StartCoroutine(mazeInstance.Generate()); //StartCoroutine starts an internal clock determins the time it takes to create the map
        mazeInstance.Generate();
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        for(int x = 0; x < numbBatteries; x++)
        {
            BatteriesInstance = Instantiate(BatteriesPreFab) as UsableItem;
            while (itemPlaced == false)
            {
                itemPlaced = BatteriesInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
            }
            itemPlaced = false;
        }
        GhostInstance = Instantiate(GhostPreFab) as GhostAi;
        while (ghostPlaced == false)
        {
            ghostPlaced = GhostInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        }

        
    }

    //function used for repopulating the game in the same play through
    /*
    private void RestartGame()
    {
        StopAllCoroutines(); //Stops the Coroutine, usefulle if the player decides to restart the map while the map is being made
        Destroy(mazeInstance.gameObject);
        if(playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
        //StartCoroutine(BeginGame());
        BeginGame();
    }
    */
}
