using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIWL : MonoBehaviour {

    public Text State;


    // initialization the text for battery life
    void Start()
    {
        State = GetComponent<Text>() as Text;
        State.enabled = false;
    }

    // Update the text based on the battery life
    void Update()
    {
        if (GameManager.win == true)
        {
            State.text = "You Win!";
            State.enabled = true;
        }
        else if (GameManager.dead == true)
        {
            State.text = "You Died";
            State.enabled = true;
        }


    }
}
