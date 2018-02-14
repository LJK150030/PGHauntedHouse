using UnityEngine;
using System.Collections;

//the instantation of an edge a player cannot pass through
public class MazeWall : MazeCellEdge {
    public Transform wall;

    //overload the initialize function for copying walls
    public override void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        base.Initialize(cell, otherCell, direction);
        wall.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
    }
}
