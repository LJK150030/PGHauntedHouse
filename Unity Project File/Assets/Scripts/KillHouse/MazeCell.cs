using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour {
    //1x1 cell of a 2D array of cells

    public IntVector2 coordinates;
    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
    private int initializedEdgeCount;
    public MazeRoom room;
    public UsableItem Item;
    public GhostAi Ghost;

    //retruns the edge of the cell
    public MazeCellEdge GetEdge (MazeDirection direction)
    {
        return edges[(int)direction];
    }

    //connects a cell to another cell based on the direction and which edge to store it
    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }

    //returns a bool if all edges have been used
    public bool IsFullyInitialized
    {
        get
        {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

    //picks a direction that has not already been used, as well as randomly leaving openings for passage ways 
    public MazeDirection RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("MazeCell has no Uninitialized directions left.");
        }
    }

    //Setting the floors to the proper color
    public void Initialize(MazeRoom room)
    {
        room.Add(this);
        transform.GetChild(0).GetComponent<Renderer>().material = room.settings.floorMaterial;
    }

    //Passes when the player enters the edge
    public void OnPlayerEntered()
    {
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i].OnPlayerEntered();
        }
    }

    //Passes when the player exits the edge
    public void OnPlayerExited()
    {
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i].OnPlayerExited();
        }
    }

    //function to set the item of the cell
    public void setItem(UsableItem item)
    {
        Item = item;
    }

    //function to set the ghosts is on the cell
    public void setGhost(GhostAi ghost)
    {
        Ghost = ghost;
    }

}
