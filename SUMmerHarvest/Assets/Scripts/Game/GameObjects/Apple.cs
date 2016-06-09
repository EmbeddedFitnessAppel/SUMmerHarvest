using UnityEngine;
using System.Collections;
using System;

public class Apple : MonoBehaviour
{


    public float KeepHanging;
    public int MinValue;
    public int MaxValue;
    public int MinRadius;
    public int MaxRadius;
    public float Speed;
    public bool UsesRigidbody;
    public int ScoreValue;
    private bool drp;
    private ScoreApple sA;
    private Rigidbody rb;

    void Start()
    {
        MinRadius = Mathf.Min(MinRadius, 1);
        MaxRadius = Mathf.Max(MaxRadius, MinRadius+1);
        MaxValue = Mathf.Max(Mathf.Max(MinValue, 1), MaxValue);
        NewScore();
        gameObject.name = "Apple "+ScoreValue;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (drp&&!UsesRigidbody)
        {
            Vector3 p = this.transform.position;
            p.Set(p.x, p.y - Speed, p.z);
            this.transform.position = p;
        }

    }

    public IEnumerator Drop()
    {
        yield return new WaitForSeconds(KeepHanging);
        DropNow();
    }

    public void DropNow()
    {
        drp = true;
        if (UsesRigidbody)
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
        return ScoreValue;
    }

    public void SetAppleUI(ScoreApple s)
    {
        sA = s;
    }
    void NewScore()
    {
        System.Random r = new System.Random();
        ScoreValue = r.Next(MinValue, MaxValue);
        if (ScoreValue == 0)
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
            //Debug.Log(gameObject.name + " fell on the floor");
            Destroy();
        }
    }
}
