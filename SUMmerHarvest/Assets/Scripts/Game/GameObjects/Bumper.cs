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

    /// <summary>
    /// This method will check for the bumpers if they hit other bumpers.
    /// If they do, The player objects respective to the bumpers should move aside.
    /// The extender collider is not used neither checked for in this method.
    /// </summary>
    /// <param name="other">The collider this bumper hits.</param>
    void OnTriggerEnter(Collider other)
    {
        if ((this.name.Contains("extender")))
        {
            return;
        }

        if (other.tag == "leftBumper" && !player.isExtended)
        {
            player.DodgeForward();
            player.isExtended = true;
            player.extendDirection = "Forward";
        }
        else if (other.tag == "rightBumper" && !player.isExtended)
        {
            player.DodgeBackward();
            player.isExtended = true;
            player.extendDirection = "Backward";
        }
    }

    /// <summary>
    /// This method will check if the extenders leave each others collider.
    /// When this happens the baskets should move back to their original place (forward baskets move backwards and vise versa).
    /// De bumper colliders will not use this trigger.
    /// </summary>
    /// <param name="other">The collider this extender leaves.</param>
    void OnTriggerExit(Collider other)
    {
        //Debug.Log("HEEOO " + other.tag + "  " + player.isExtended + "  " + player.extendDirection);
        if (other.tag == "extender" && player.isExtended == true && !(this.name.Contains("Bumper")))
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
