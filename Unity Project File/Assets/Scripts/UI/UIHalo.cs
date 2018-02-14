using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHalo : MonoBehaviour {

    public float playerHealth;
    private float playerHealthDef;
    public float percentDiff;
    private Image pic;

    // Use this for initialization
    void Start () {
        playerHealth = Player.health;
        playerHealthDef = playerHealth;
        pic = GetComponent<Image>() as Image;
    }
	
	// Update is called once per frame
	void Update () {
        playerHealth = Player.health;
        percentDiff = ((Mathf.Abs(playerHealthDef - playerHealth) / ((playerHealthDef + playerHealth) / (2))) * 200)/255;
        pic.color = new Vector4(0.06274509803f, 0.0f, 0.14117647058f, percentDiff);
    }
}
