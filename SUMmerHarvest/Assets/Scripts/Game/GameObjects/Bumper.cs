using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {

    private Basket player;

    private bool isExtended;
    private string extendDirection;

	// Use this for initialization
	void Start () {
        player = GetComponentInParent<Basket>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "leftBumper" && !isExtended)
        {
            player.DodgeForward();
            isExtended = true;
            extendDirection = "Forward";
        }
        else if (other.tag == "rightBumper" && !isExtended)
        {
            player.DodgeBackward();
            isExtended = true;
            extendDirection = "Backward";
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("HEEOO " + other.tag);
        if (other.tag == "extender" && isExtended == true)
        {
            if(extendDirection == "Forward")
            {
                player.DodgeBackward();
            }
            else if(extendDirection == "Backward")
            {
                player.DodgeForward();
            }
            isExtended = false;
        }
    }
}
