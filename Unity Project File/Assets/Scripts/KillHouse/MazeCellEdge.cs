using UnityEngine;

//Keeping track of the connection between cells by giving each cell their own unidirectional edge.
//We never have an edge called MazeCellEdge, thus making the class abstract
public abstract class MazeCellEdge : MonoBehaviour
{
    public MazeCell cell, otherCell;
    public MazeDirection direction;

    public virtual void OnPlayerEntered() { }
    public virtual void OnPlayerExited() { }

    //creates the edge between two cells and makes them a child of the Cell
    public virtual void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        this.cell = cell;
        this.otherCell = otherCell;
        this.direction = direction;
        cell.SetEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
    }
}


