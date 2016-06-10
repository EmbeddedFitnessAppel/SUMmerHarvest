using UnityEngine;
using System.Collections;

public abstract class Player : MonoBehaviour {

    public enum Direction { None, Left, Right, Up, Down };
    /// <summary>
    ///     If true, local input (actions received from <see cref="Input" /> api) is ignored.
    /// </summary>
    public bool DisableLocalInput;
    private Rigidbody body;
    public string Name { get; private set; }
    public float Speed = 12.0f;
    public int PlayerNumber;

    public Rigidbody Body
    {
        get { return body ?? (body = GetComponent<Rigidbody>()); }
    }

}
