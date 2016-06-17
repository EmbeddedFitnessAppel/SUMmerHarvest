using Assets.Scripts.Game.GameObjects;
using UnityEngine;
using UnityEngine.UI;

public class Monkey : Player
{
    //Speed moved to Player.cs
    //public float Speed = 2.5f;
    public float BackToAreaSpeed = 20f;
    public float ArmStrengthUp = 10f;
    public float ArmStrengthDown = 15f;
    public Transform CenterMoveArea;
    public float SlamRange = 2.5f;
    private GameObject appleRange;

    private bool moveToCenter;


    void Start() {
        //Debug.LogError("Monkey range moet nog de kleur krijgen van het team waar ze in zitten!!!");//dit moet je natuurlijk weghalen wanneer je
        //appleRange.GetComponent<Image>().color = new Color(1F,1F,1F,0.5F);
    }

    private void Awake()
    {
    }

    public void FixedUpdate()
    {
        if (DisableLocalInput) return;


        Body.AddForce(new Vector3(Input.GetAxis("Monkey" + PlayerNumber + "Horizontal") * Speed,
            Input.GetAxis("Monkey" + PlayerNumber + "Vertical") * Speed));
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

    private void Update()
    {
        if (!appleRange)
        {
            Debug.LogWarning("Please add a monkey range prefab to the GameManager.");
        }
        else
        {
            appleRange.transform.position = transform.position;
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
        print("monkey slam" + PlayerNumber);
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

    public override void SetColor(Color c) {
        appleRange = GameManager.Instance.CreateMonkeyInRangeIndicator();
        appleRange.GetComponent<Image>().transform.localScale = new Vector3(SlamRange, SlamRange);

        appleRange.gameObject.GetComponent<Image>().color = new Color(c.r, c.g, c.b, .5f);
    }
}