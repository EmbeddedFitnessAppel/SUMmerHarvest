using UnityEngine;
using System.Collections;

public abstract class Player : MonoBehaviour {
    private Rigidbody body;
    public string Name { get; private set; }

    public Rigidbody Body
    {
        get { return body ?? (body = GetComponent<Rigidbody>()); }
    }
}
