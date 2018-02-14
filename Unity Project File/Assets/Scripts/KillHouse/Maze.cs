using UnityEngine;
using System.Collections;
using System.Collections.Generic; //allosing a list for a generic type

public class Maze : MonoBehaviour {
    //Constructing of the procedual generated map

    public float generationStepDelay; //Debuging to see how the map is formed
                                      //    public int sizeX, sizeZ;        //the size of the entire map
    public MazeCell cellPrefab;     //preFab of one square on the map
    public IntVector2 size;         //a 2D vector on how large the map is
    private MazeCell[,] cells;      //preFab of all squares on the map
    public MazePassage passagePrefab;
    public MazeWall[] wallPrefabs;
    public MazeDoor doorPrefab;
    public MazeRoomSettings[] roomSettings;
    private List<MazeRoom> rooms = new List<MazeRoom>();

    [Range(0.0f, 1.0f)]
    public float doorProbability;

    public IntVector2 getMazeSize()
    {
        return size;
    }

    //returns a cell from a given coordinate
    public MazeCell GetCell(IntVector2 coordinates)
    {
        return cells[coordinates.x, coordinates.z];
    }

    /// populates the entire map and holds the data using a 2D array
    /// IEnumerator slows the function down, allowying the user to see how the map is generated
    public void Generate()
    {
        //WaitForSeconds delay = new WaitForSeconds(generationStepDelay); //Instantiates the delay time
        cells = new MazeCell[size.x, size.z]; //Instantiates the maze database
        List<MazeCell> activeCells = new List<MazeCell>(); //Creates a temporary list to hold the cells that were just created
        DoFirstGenerationStep(activeCells); 
        
        while (activeCells.Count > 0) //loops until the coordinates points in the direction of a previous cell
        {
            //yield return delay;
            DoNextGenerationStep(activeCells);
            
            /*
            yield return delay; //Delays the construction of the maze
            CreateCell(coordinates); //Constructs a new cell with the given location
            coordinates += MazeDirections.RandomValue.ToIntVector2(); //Constructs a new 2D vector with a random direction
            */
        }


        /* Linear generation of the 2D array
           for (int x = 0; x < size.x; x++)
               for (int z = 0; z < size.z; z++)
               {
                   yield return delay;
                   CreateCell(new IntVector2(x, z));
               }
       */
    }

    //Starts the process by adding a new starting point into the currently active cells
    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        MazeCell newCell = CreateCell(RandomCoordinates);
        newCell.Initialize(CreateRoom(-1));
        activeCells.Add(newCell);
    }

    //Continues the porocess of randomly creating cells, check wheter if the current cell is in the list, and if not then removing the cell from the list
    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        //checks to see if the cell is fully Initialized
        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2(); //Instantiates a random point to start at
        //Checking neighboring cells, creating passages, then creating walls around the created cells
        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else if (currentCell.room.settingsIndex == neighbor.room.settingsIndex)
            {
                CreatePassageInSameRoom(currentCell, neighbor, direction);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    //Creates a randome 2D vector from within the given map size
    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0, size.x), Random.Range(0, size.z));
        }
    }

    //Checks to see if the 2D vector is within the given play area.
    public bool ContainsCoordinates (IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }



    // Creates a single cell that will be Instantiated in the game and in the 2D array
    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform; //centers the parent object with the new placement
        newCell.transform.localPosition = new Vector3(coordinates.x - (size.x*0.5f) + 0.5f, 0f, coordinates.z - (size.z*0.5f) + 0.5f); //going from negative 10 to positive 10 in both x and z direction
        return newCell;
    }

    //Creates a passage between two edes and Instantiates the createion of the passage
    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
        MazePassage passage = Instantiate(prefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as MazePassage;
        if (passage is MazeDoor)
        {
            otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
        }
        else
        {
            otherCell.Initialize(cell.room);
        }
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    //Creates a wall between two edges and instantiates the creation of the wall
    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if(otherCell != null)
        {
            wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)]) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    //creates a new room based on the number of rooms provided
    private MazeRoom CreateRoom(int indexToExclude)
    {
        MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
        newRoom.settingsIndex = Random.Range(0, roomSettings.Length);
        if (newRoom.settingsIndex == indexToExclude)
        {
            newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
        }
        newRoom.settings = roomSettings[newRoom.settingsIndex];
        rooms.Add(newRoom);
        return newRoom;
    }

    //Creates a passage from within the same room
    private void CreatePassageInSameRoom (MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
        if(cell.room != otherCell.room)
        {
            MazeRoom roomToAssimilate = otherCell.room;
            cell.room.Assimilate(roomToAssimilate);
            rooms.Remove(roomToAssimilate);
            Destroy(roomToAssimilate);
        }
    }

}
