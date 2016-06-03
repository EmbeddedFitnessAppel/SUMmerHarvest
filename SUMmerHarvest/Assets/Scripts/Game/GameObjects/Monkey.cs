using UnityEngine;

public class Monkey : Player
{
    public float Speed = 2.5f;
    public float BackToAreaSpeed = 20f;
    public float ArmStrengthUp = 10f;
    public float ArmStrengthDown = 15f;
    public Transform CenterMoveArea;
    public float SlamRange = 2.5f;

    private bool moveToCenter;

    private void Awake()
    {
        transform.position = CenterMoveArea.position;
    }

    private void FixedUpdate()
    {
        Move(Direction.Up);

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

    /// <summary>
    ///     Moves the Mockey towards the right direction.
    /// </summary>
    /// <param name="direction">Left, Right, Up or Down</param>
    public override void Move(Direction direction)
    {
        // Allow movement.
        Body.AddForce(new Vector3(Input.GetAxis("Horizontal") * Speed, Input.GetAxis("Vertical") * Speed));
    }

    private void Slam()
    {
        foreach (var target in Physics.OverlapSphere(transform.position, SlamRange))
        {
            if (!target.CompareTag("Apple")) continue;
            var apple = target.GetComponent<Apple>();
            if (apple.IsFalling) continue;

            apple.DropNow();

            // Apply arm force.
            var isSlammingUp = Mathf.Sign(Input.GetAxis("MonkeySlam")) == 1;
            var horizontalSlampForce = isSlammingUp ? Mathf.Sign(Random.value * 2 - 1f) * (ArmStrengthUp / 3f) : 0f;
            apple.GetComponent<Rigidbody>()
                .AddForce(
                    new Vector3(horizontalSlampForce,
                        Input.GetAxis("MonkeySlam") * (isSlammingUp ? ArmStrengthUp : ArmStrengthDown), 0),
                    ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SlamRange);
    }
}