using UnityEngine;
using System.Collections;

public class Battery_Life : MonoBehaviour {

    public static float bLife = 100.0f;
    public float drain = 0.5f;
    public Light flashLight;
	
	// Updates the draing of using the flashlight
	void Update () {
	    if(bLife <= 0.0)
        {
            flashLight.enabled  = false;
        }
        else
        {
            bLife = bLife - Time.deltaTime*drain;
        }
	}
}
