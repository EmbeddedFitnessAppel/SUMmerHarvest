using System;
using UnityEngine;

public class Monkey : Player
{
    public float Speed = 2.5f;
    public float BackToAreaSpeed = 20f;
    public Transform CenterMoveArea;
    public float SlamRange = 2.5f;

    private bool moveToCenter;

    private void Awake()
    {
        transform.position = CenterMoveArea.position;
    }

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

        if (Input.GetButtonDown("MonkeySlam"))
        {
            Slam();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CenterMoveArea.CompareTag(other.tag)) return;
        moveToCenter = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CenterMoveArea.CompareTag(other.tag)) return;
        moveToCenter = true;
    }

    private void Slam()
    {
        foreach (var target in Physics.OverlapSphere(transform.position, SlamRange))
        {
            if (!target.CompareTag("Apple")) continue;
            var apple = target.GetComponent<Apple>();
            if (apple.IsFalling) continue;

            // TODO: Apply force to apple to move it up or down with: Input.GetAxis("MonkeySlam").
            apple.DropNow();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SlamRange);
    }
}