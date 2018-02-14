using UnityEngine;
using System.Collections;

public class Flashlight : MonoBehaviour {

    public bool inLight = false;

    //When the ghost is spotted with the flash light
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ghost")
        {
            inLight = true;
        }
    }

    //When the ghost is still in the light, the ghost will take damage
    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Ghost")
        {
            if (col.GetComponentInParent<GhostAi>().seePlayer() == true)
                col.GetComponentInParent<GhostAi>().takeDamage(Time.deltaTime * 1);
        }
    }

    //When the ghost is no longer in the flash light
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ghost")
        {
            inLight = false;
        }
    }
}
