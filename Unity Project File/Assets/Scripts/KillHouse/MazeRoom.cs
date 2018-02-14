using UnityEngine;
using System.Collections.Generic;

//Used to reference the rooms generated on the map
public class MazeRoom : ScriptableObject
{

    public int settingsIndex;

    public MazeRoomSettings settings;

    private List<MazeCell> cells = new List<MazeCell>();

    //adding a cell to a room
    public void Add(MazeCell cell)
    {
        cell.room = this;
        cells.Add(cell);
    }

    //making the room larger
    public void Assimilate (MazeRoom room)
    {
        for(int i = 0; i < room.cells.Count; i++)
        {
            Add(room.cells[i]);
        }
    }
}
