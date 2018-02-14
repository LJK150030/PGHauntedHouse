using UnityEngine;
using System.Collections;
[System.Serializable]

//A new structer that create a 2D vector in the X and Z direction with integer values
public struct IntVector2{
    
    public int x, z;

    //Constructor of an IntVector2
    public IntVector2 (int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    //overides the addition operator when using two IntVectors
    public static IntVector2 operator + (IntVector2 a, IntVector2 b)
    {
        a.x += b.x;
        a.z += b.z;
        return a;
    }

}
