using UnityEngine;

public class Monkey : Player {
    public float Speed = 2.5f;
    public float BackToAreaSpeed = 20f;
    public Transform CenterMoveArea;
    public float SlamRange = 2.5f;

    private bool moveToCenter;

    private void Awake() {
        transform.position = CenterMoveArea.position;
    }

    private void FixedUpdate() {
        Move(Direction.Up);

        // Move back towards the center of the tree when you get out of it's bounds
        if (moveToCenter) {
            var directionVector = CenterMoveArea.position - transform.position;
            Body.AddForce(directionVector.normalized * BackToAreaSpeed);
        }

        if (Input.GetButtonDown("MonkeySlam")) {
            Slam();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!CenterMoveArea.CompareTag(other.tag)) return;
        moveToCenter = false;
    }

    private void OnTriggerExit(Collider other) {
        if (!CenterMoveArea.CompareTag(other.tag)) return;
        moveToCenter = true;
    }

    /// <summary>
    ///     Moves the Mockey towards the right direction.
    /// </summary>
    /// <param name="direction">Left, Right, Up or Down</param>
    public override void Move() {
        // Calculate the direction of the movement, normalize that vector and multiply by speed.
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement.Normalize();
        movement *= Speed;

        Body.AddForce(movement);
    }

    private void Slam() {
        foreach (var target in Physics.OverlapSphere(transform.position, SlamRange)) {
            if (!target.CompareTag("Apple")) continue;
            var apple = target.GetComponent<Apple>();
            if (apple.IsFalling) continue;

            // TODO: Apply force to apple to move it up or down with: Input.GetAxis("MonkeySlam").
            apple.DropNow();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, SlamRange);
    }
}