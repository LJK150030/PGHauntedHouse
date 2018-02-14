using UnityEngine;
using System.Collections;

public class GhostAi : MonoBehaviour {

    public float notCloseBy = 10.0f;
    private float notCloseByDef;
    public bool inTrigger = false;
    public MazeCell usedCell;
    public bool ghostPlaced = false;
    public float health = 2.0f;
    private float healthDef;
    public bool seeP = false;
    

    void Start()
    {
        notCloseByDef = notCloseBy;
        healthDef = health;
        ghostPlaced = true;
        seeP = seePlayer();
    }
	
	// Ghost's Ai for constantly looking at the player or when to change locations
	void Update () {
	    if(inTrigger == false)
        {
            notCloseBy = notCloseBy - Time.deltaTime;
            if (notCloseBy <= 0)
            {
                ghostPlaced = false;
                while (ghostPlaced == false)
                {
                    ghostPlaced = this.SetLocation(GameManager.mazeInstance.GetCell(GameManager.mazeInstance.RandomCoordinates));
                }
                ghostPlaced = true;
                notCloseBy = notCloseByDef;
            }
        }
        else if (health <= 0)
        {
            death();
        }
        
        
	}

    //when the player enters the ghost's triger, the ghost will rotate in the direction of the player
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            notCloseBy = notCloseByDef;
            inTrigger = true;
        }

    }

    //if the player is still in the trigger, the ghost will continue look at the player, but if the ghost can "see" the player, the player will take damage
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            this.transform.GetChild(0).transform.LookAt(col.transform.position);
            inTrigger = true;
            if (seePlayer())
            {
                col.GetComponent<Player>().takeDamage(Time.deltaTime * 1);
            }
            notCloseBy = notCloseByDef;
        }
    }

    //when the player is out of range of the ghost, the ghost will wait to either the player comes back into the trigger or respawn to a new location
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            inTrigger = false;
        }
    }

    //sets the ghost's location via cordinates
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

    //ghost will lose health
    public void takeDamage(float amount)
    {
        health = health - amount;
    }

    //if the ghost looses health, the ghost will disaprear and reaprear in a diffrent location
    public void death()
    {
        ghostPlaced = false;
        while (ghostPlaced == false)
        {
            ghostPlaced = this.SetLocation(GameManager.mazeInstance.GetCell(GameManager.mazeInstance.RandomCoordinates));
        }
        ghostPlaced = true;
        notCloseBy = notCloseByDef;
        health = healthDef;
    }

    //sends a ray cast to see if the ghost has line of sight of the player
    public bool seePlayer()
    {
        Ray landingRay = new Ray(this.transform.GetChild(0).transform.position, transform.GetChild(0).transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        Debug.DrawRay(this.transform.GetChild(0).transform.position, transform.GetChild(0).transform.TransformDirection(Vector3.forward));
        if (Physics.Raycast(landingRay, out hit, 100.0f))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.Log(hit.collider.name);
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
}
