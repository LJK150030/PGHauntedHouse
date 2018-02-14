using UnityEngine;

//The associated directions for 2D vectors
public enum MazeDirection {
    North,
    East,
    South,
    West
}

// used to randomly elect a direction
public static class MazeDirections
{
    public const int Count = 4;

    //Converts the cordinate base on the direction of NSEW
    private static IntVector2[] vectors =
    {
        new IntVector2(0, 1), //North
        new IntVector2(1, 0), //East
        new IntVector2(0, -1),//South
        new IntVector2(-1, 0) // West
    };

    //Converts an arbitarty enum direction into an integer vector (someDirection.ToIntVector2())
    public static IntVector2 ToIntVector2 (this MazeDirection direction)
    {
        return vectors[(int)direction];
    }
    //Array of oposite directions
    private static MazeDirection[] opposites =
{
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East
    };

    //returns the oposite direction of a given direction
    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return opposites[(int)direction];
    }

    //Array of Quaternion rotations with respectable direction
    private static Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0.0f, 90.0f, 0.0f),
        Quaternion.Euler(0.0f, 180.0f, 0.0f),
        Quaternion.Euler(0.0f, 270.0f, 0.0f)
    };

    //returns the tranform rotation of given direction
    public static Quaternion ToRotation (this MazeDirection direction)
    {
        return rotations[(int)direction];
    }

    //Returns a random direction
    public static MazeDirection RandomValue
    {
        get
        {
            return (MazeDirection)Random.Range(0, Count);
        }
    }


}
