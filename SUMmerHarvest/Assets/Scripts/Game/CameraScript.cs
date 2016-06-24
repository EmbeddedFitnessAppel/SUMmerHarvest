using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    private AudioSource fxSound;

    // Use this for initialization
    void Start () {
        fxSound = GetComponent<AudioSource>();
        fxSound.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
