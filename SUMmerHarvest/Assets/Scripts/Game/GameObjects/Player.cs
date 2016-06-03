using UnityEngine;
using System.Collections;

public abstract class Player : MonoBehaviour {

    public enum Direction { Left, Right, Up, Down };

    private Rigidbody body;
    public string Name { get; private set; }

    public Rigidbody Body
    {
        get { return body ?? (body = GetComponent<Rigidbody>()); }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Direction.Left);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Direction.Right);
        }
    }

    /// <summary>
    /// Moves the player towards the right direction.
    /// </summary>
    /// <param name="direction">Left, Right, Up or Down</param>
    public abstract void Move(Direction direction);

}
