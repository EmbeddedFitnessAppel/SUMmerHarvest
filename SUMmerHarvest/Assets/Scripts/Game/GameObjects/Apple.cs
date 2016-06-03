using UnityEngine;
using Assets.Scripts.helpers;
using System.Collections;
using System;

public class Apple : MonoBehaviour
{


    public float keepHanging;
    public int minValue;
    public int maxValue;
    public int minRadius;
    public int maxRadius;
    public float speed;
    public bool usesRigidbody;
    public int scoreValue;
    private bool drp;
    private ScoreApple sA;
    private Rigidbody rb;

    void Start()
    {
        minRadius = Number.AssertMinInt(minRadius, 1);
        maxRadius = Number.AssertMinInt(maxRadius, minRadius+1);
        maxValue = Number.AssertMinInt(maxValue, minValue+1);
        NewScore();
        gameObject.name = "Apple "+scoreValue;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (drp&&!usesRigidbody)
        {
            Vector3 p = this.transform.position;
            p.Set(p.x, p.y - speed, p.z);
            this.transform.position = p;
        }

    }

    public IEnumerator Drop()
    {
        yield return new WaitForSeconds(keepHanging);
        DropNow();
    }

    public void DropNow()
    {
        drp = true;
        if (usesRigidbody)
        {
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }

    public void Pickup(Basket b)
    {
        b.CatchApple(this);
        Destroy();
    }

    public void Destroy()
    {
        GameObject.Destroy(sA.gameObject);
        GameObject.Destroy(this.gameObject);
    }
    public int GetNumber()
    {
        return scoreValue;
    }

    public void SetAppleUI(ScoreApple s)
    {
        sA = s;
    }
    void NewScore()
    {
        System.Random r = new System.Random();
        scoreValue = r.Next(minValue, maxValue);
        if (scoreValue == 0)
        {
            NewScore();
        }
    }
    public bool IsFalling
    {
        get { return drp; }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "basket")
        {
            Pickup(other.GetComponentInChildren<Basket>());
        }
        if(other.tag=="floor")
        {
            Debug.Log(gameObject.name + " fell on the floor");
            Destroy();
        }
    }
}
