using UnityEngine;
using System.Collections;

public abstract class Player : MonoBehaviour {

    public enum Direction { None, Left, Right, Up, Down };

    private Rigidbody body;
    public string Name { get; private set; }
    public float Speed = 12.0f;

    public Rigidbody Body
    {
        get { return body ?? (body = GetComponent<Rigidbody>()); }
    }

}
