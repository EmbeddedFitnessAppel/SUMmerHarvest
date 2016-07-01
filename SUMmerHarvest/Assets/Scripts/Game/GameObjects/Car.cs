using UnityEngine;
using System.Collections;

public class Car : MonoBehaviour {

    NavMeshAgent agent;
    public int DestPoint = 0;
    public Transform navPoints;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();

        GotoNextPoint();
	}
	
	// Update is called once per frame
	void Update () {
        if (agent.remainingDistance < 0.5f)
            GotoNextPoint();
	}

    void GotoNextPoint()
    {
        if (navPoints.childCount <= 0) return;

        agent.destination = navPoints.GetChild(DestPoint).position;

        DestPoint = (DestPoint + 1) % navPoints.childCount;
    }
}
