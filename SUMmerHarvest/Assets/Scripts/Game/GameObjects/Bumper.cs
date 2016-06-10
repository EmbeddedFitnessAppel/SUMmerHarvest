using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {

    private Basket player;

	// Use this for initialization
	void Start () {
        player = GetComponentInParent<Basket>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "leftBumper" && !player.isExtended)
        {
            Debug.Log("Bwaaa");
            player.DodgeForward();
            player.isExtended = true;
            player.extendDirection = "Forward";
        }
        else if (other.tag == "rightBumper" && !player.isExtended)
        {
            Debug.Log("Bwaaa2");
            player.DodgeBackward();
            player.isExtended = true;
            player.extendDirection = "Backward";
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("HEEOO " + other.tag + "  " + player.isExtended + "  " + player.extendDirection);
        if (other.tag == "extender" && player.isExtended == true)
        {
            if(player.extendDirection == "Forward")
            {
                player.DodgeBackward();
            }
            else if(player.extendDirection == "Backward")
            {
                player.DodgeForward();
            }
            player.isExtended = false;
        }
    }
}
