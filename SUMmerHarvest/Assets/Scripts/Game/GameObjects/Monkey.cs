using UnityEngine;

public class Monkey : Player
{
    //Speed moved to Player.cs
    //public float Speed = 2.5f;
    public float BackToAreaSpeed = 20f;
    public float ArmStrengthUp = 10f;
    public float ArmStrengthDown = 15f;
    public Transform CenterMoveArea;
    public float SlamRange = 2.5f;

    private bool moveToCenter;

    private void Awake()
    {
    }

    public void FixedUpdate()
    {
        if (DisableLocalInput) return;

        Body.AddForce(new Vector3(Input.GetAxis("Player" + PlayerNumber + "Horizontal") * Speed,
            Input.GetAxis("Player" + PlayerNumber + "Vertical") * Speed));

        // Move back towards the center of the tree when you get out of it's bounds
        if (moveToCenter)
        {
            var directionVector = CenterMoveArea.position - transform.position;
            Body.AddForce(directionVector.normalized * BackToAreaSpeed);
        }

        if (Input.GetButtonDown("Player" + PlayerNumber + "MonkeySlam"))
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

            apple.DropNow();

            // Apply arm force.
            var isSlammingUp = Mathf.Sign(Input.GetAxis("Player" + PlayerNumber + "MonkeySlam")) == 1;
            var horizontalSlampForce = isSlammingUp ? Mathf.Sign(Random.value * 2 - 1f) * (ArmStrengthUp / 3f) : 0f;
            var force = new Vector3(horizontalSlampForce,
                Input.GetAxis("Player" + PlayerNumber + "MonkeySlam") * (isSlammingUp ? ArmStrengthUp : ArmStrengthDown),
                0);
            apple.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SlamRange);
    }
}