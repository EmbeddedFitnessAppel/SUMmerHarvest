using UnityEngine;
using Assets.Scripts.helpers;
using System.Collections;
using System;

public class Apple : MonoBehaviour
{


    public int keepHanging;
    public int minValue;
    public int maxValue;
    public int minRadius;
    public int maxRadius;
    public float speed;
    private int scoreValue;
    private int h = 0;
    private bool drp;
    private ScoreApple sA;

    void Start()
    {
        minRadius = Number.AssertMinInt(minRadius, 1);
        maxRadius = Number.AssertMinInt(maxRadius, minRadius+1);
        maxValue = Number.AssertMinInt(maxValue, minValue+1);
        NewScore();
        gameObject.name = "Apple "+scoreValue;
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
    public bool IsFalling()
    {
        return drp;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="floor")
        {
            Debug.Log(gameObject.name + " fell on the floor");
            Destroy();
        }
    }
}
