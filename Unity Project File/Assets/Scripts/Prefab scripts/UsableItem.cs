using UnityEngine;
using System.Collections;

public class UsableItem : MonoBehaviour {

    public MazeCell usedCell;

    //Picking up the Battery and replenashing the battery life
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            Destroy(gameObject);
            Battery_Life.bLife = 100.0f;
            GameManager.numbBatteries = GameManager.numbBatteries - 1;
        }
    }

    //sets the location via cell cordinates
    public bool SetLocation(MazeCell cell)
    {
        usedCell = cell;
        if (usedCell.Item != null)
        {
            return false;
        }
        else
        {
            this.transform.localPosition = cell.transform.localPosition;
            return true;
        }
    }


}
