using UnityEngine;

public class Player : MonoBehaviour
{

    public MazeCell currentCell;
    public Maze playArea;
    public GameObject Stand;
    private GameObject StandInstance;

    private int currentCellX;
    private int currentCellZ;

    private float currentCorX;
    private float currentCorZ;
    private int currentCorIntX;
    private int currentCorIntZ;
    private int currentCorIntLocX;
    private int currentCorIntLocZ;

    public static float health = 25;

    //sets the location of the player at the begining of the game
    public void SetLocation(MazeCell cell)
    {
        if (currentCell != null)
        {
            currentCell.OnPlayerExited();
        }
        currentCell = cell;
        this.transform.localPosition = currentCell.transform.localPosition;
        currentCorX = this.transform.localPosition.x + playArea.size.x / 2;
        currentCorZ = this.transform.localPosition.z + playArea.size.z / 2;
        currentCellX = currentCell.coordinates.x;
        currentCellZ = currentCell.coordinates.z;
        StandInstance = Instantiate(Stand);
        StandInstance.transform.position = new Vector3(currentCell.transform.localPosition.x - 0.5f, 0.0f, currentCell.transform.localPosition.z - 0.5f);

        currentCell.OnPlayerEntered();
    }
     //updates the players location based on cordinates
    public void SetLocationA(MazeCell cell)
    {
        if (currentCell != null)
        {
            currentCell.OnPlayerExited();
        }
        currentCell = cell;
        currentCellX = currentCell.coordinates.x;
        currentCellZ = currentCell.coordinates.z;

        currentCell.OnPlayerEntered();
    }

    //moves the stand's location based on the players cordinates
    private void Move(MazeDirection direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction); //Returns the edge 
        if (edge is MazePassage)
        {
            SetLocationA(edge.otherCell); //Returns the cell on the other side of the door
        }
    }

    //last update of the game
    private void LateUpdate()
    {
        if(health <= 0.0f  || Battery_Life.bLife <= 0.0f || GameManager.numbBatteries <= 0)
        {
            Destroy(this.gameObject);
        }
        currentCorX = this.transform.localPosition.x + playArea.size.x / 2;
        currentCorZ = this.transform.localPosition.z + playArea.size.z / 2;
        currentCorIntX = (int) currentCorX;
        currentCorIntZ = (int) currentCorZ;
        StandInstance.transform.position = new Vector3(currentCorIntX - playArea.size.x / 2, 0.0f, currentCorIntZ - playArea.size.z / 2);
        currentCell = GameObject.Find("Maze Cell " + currentCorIntX + ", " + currentCorIntZ).GetComponent<MazeCell>();

        SetLocationA(currentCell);
    }

    //player will lose health
    public void takeDamage(float amount)
    {
        health -= amount;
    }
}