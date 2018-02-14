using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBattery_Life : MonoBehaviour {

    public Text counterText;
    public float percent = 0.0f;


	// initialization the text for battery life
	void Start () {
        percent = Battery_Life.bLife;
        counterText = GetComponent<Text>() as Text;
    }
	
	// Update the text based on the battery life
	void Update () {
        percent = Battery_Life.bLife;
        counterText.text = "Batteries: " + percent.ToString("00") + "%";
        
    }
}
