using Assets.Scripts.helpers;
using UnityEngine;
using Random = System.Random;

public class Apple : MonoBehaviour
{
    public float keepHanging;
    public int minValue;
    public int maxValue;
    public int minRadius;
    public int maxRadius;
    public float speed;
    public bool usesRigidbody;
    private int scoreValue;
    private int h;
    private ScoreApple sA;
    private Rigidbody rb;

    public bool IsFalling { get; private set; }

    private void Start()
    {
        minRadius = Number.AssertMinInt(minRadius, 1);
        maxRadius = Number.AssertMinInt(maxRadius, minRadius + 1);
        maxValue = Number.AssertMinInt(maxValue, minValue + 1);
        NewScore();
        gameObject.name = "Apple " + scoreValue;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (drp&&!usesRigidbody)
        {
            var p = transform.position;
            p.Set(p.x, p.y - speed, p.z);
            transform.position = p;
        }
    }

    public IEnumerator Drop()
    {
        yield return new WaitForSeconds(keepHanging);
        drp = true;
        if(usesRigidbody)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    public void Pickup(Basket b)
    {
        b.CatchApple(this);
        Destroy();
    }

    public void Destroy()
    {
        Destroy(sA.gameObject);
        Destroy(gameObject);
    }

    public int GetNumber()
    {
        return scoreValue;
    }

    public void SetAppleUI(ScoreApple s)
    {
        sA = s;
    }

    private void NewScore()
    {
        var r = new Random();
        scoreValue = r.Next(minValue, maxValue);
        if (scoreValue == 0)
        {
            NewScore();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "floor")
        {
            Debug.Log(gameObject.name + " fell on the floor");
            Destroy();
        }
    }
}