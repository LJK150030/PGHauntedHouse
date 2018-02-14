using UnityEngine;


public class MazeDoor : MazePassage {

    public Transform hinge;

    private MazeDoor OtherSideOfDoor
    {
        get
        {
            return otherCell.GetEdge(direction.GetOpposite()) as MazeDoor;
        }
    }

    //overides the initialize function to allow access the cell on the otherside of door 
    public override void Initialize(MazeCell primary, MazeCell other, MazeDirection direction)
    {
        base.Initialize(primary, other, direction); //calls the Initial Initialize, thus allowing us to check if the door has alread been made
        //If there is something on the other side of the door, then we move the hinge, allowing the door to be flip open on the same side relative to its position.
        if(OtherSideOfDoor != null)
        {
            hinge.localScale = new Vector3(-1f, 1f, 1f);
            Vector3 p = hinge.localPosition;
            p.x = -p.x;
            hinge.localPosition = p;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child != hinge)
            {
                child.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;
            }
        }
    }

    //when the player is in front of the door, the door will open
    public override void OnPlayerEntered()
    {
        OtherSideOfDoor.hinge.localRotation = hinge.localRotation = Quaternion.Euler(0f, -90f, 0f);
    }

    //when the player is on the other side of the same door, the door will close behind.
    public override void OnPlayerExited()
    {
        OtherSideOfDoor.hinge.localRotation = hinge.localRotation = Quaternion.identity;
    }
}
