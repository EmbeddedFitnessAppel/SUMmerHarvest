using UnityEngine;
using System.Collections;

public abstract class Player : MonoBehaviour {

    public enum Direction { Left, Right, Up, Down };

    private Rigidbody body;
    public string Name { get; private set; }
    private float speed = 12.0f;

    public Rigidbody Body
    {
        get { return body ?? (body = GetComponent<Rigidbody>()); }
    }

    //Handles the movement input.
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Direction.Left, speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Direction.Right, speed);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(Direction.Up, speed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(Direction.Down, speed);
        }
    }

    /// <summary>
    /// Moves the player towards the right direction.
    /// </summary>
    /// <param name="direction">Left, Right, Up or Down</param>
    /// <param name="speed">The speed of the player</param>
    public abstract void Move(Direction direction, float speed);

}
