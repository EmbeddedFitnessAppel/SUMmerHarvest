using UnityEngine;
using System.Collections;
using System;

public class Apple : MonoBehaviour
{


    public int keepHanging;
    public int minValue;
    public int maxValue;
    public float speed;
    public int scoreValue;
    private int h = 0;
    private bool drp;
    private scoreApple sA;

    void Start()
    {
        NewScore();
    }
    void Update()
    {
        if (!drp)
        {
            if (h <= keepHanging)
            {
                h++;
            }
            else
            {
                Drop();
            }
        }
        else
        {
            Vector3 p = this.transform.position;
            p.Set(p.x, p.y - speed, p.z);
            this.transform.position = p;
        }
    }

    public void Drop()
    {
        drp = true;
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

    public void SetAppleUI(scoreApple s)
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
    public bool IsFalling()
    {
        return drp;
    }
}
