using System;
using UnityEngine;

public class Monkey : Player
{
    public float Speed = 2.5f;
    public float BackToAreaSpeed = 20f;
    public Transform CenterMoveArea;

    private bool moveToCenter;

    private void FixedUpdate()
    {
        // Allow movement.
        Body.AddForce(new Vector3(Input.GetAxis("Horizontal") * Speed, Input.GetAxis("Vertical") * Speed));

        if (moveToCenter)
        {
            // Move back to the tree.
            var directionVector = CenterMoveArea.position - transform.position;
            Body.AddForce(directionVector.normalized * BackToAreaSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        moveToCenter = false;
    }

    private void OnTriggerExit(Collider other)
    {
        moveToCenter = true;
    }

    /// <summary>
    /// Moves the Mockey towards the right direction.
    /// </summary>
    /// <param name="direction">Left, Right, Up or Down</param>
    public override void Move(Direction direction)
    {
        throw new NotImplementedException();
    }
}