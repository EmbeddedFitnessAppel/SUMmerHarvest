using UnityEngine;
using System.Collections;

public class MonkeyRange : MonoBehaviour {

	void Start () { Monkey m =  this.GetComponentInParent<Monkey>();
    if (m != null) this.GetComponent<SphereCollider>().radius = m.SlamRange;
    else Debug.LogError("This monkey range does not have a parent object with a monkey script");
	}

}
