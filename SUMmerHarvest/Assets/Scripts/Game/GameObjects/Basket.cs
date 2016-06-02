using UnityEngine;
using System.Collections;

public class Basket : Player {
    //public KeyCode kL;
    //public KeyCode kR;
    //public float speed;
    //private Vector3 c;

    //private int score;

    //void Start()
    //{
    //    c = transform.position;
    //    score = 160;
    //}

    //void Update()
    //{
    //    if (Input.GetKey(kL))
    //    {
    //        c.x = c.x - speed;
    //    }
    //    else if (Input.GetKey(kR))
    //    {
    //        c.x = c.x + speed;
    //    }

    //    transform.position = c;
    //}
    //void OnTriggerEnter(Collider col)
    //{
    //    if (col.gameObject.tag == "pickup")
    //    {
    //        Apple a = col.gameObject.GetComponent<Apple>();
    //        a.Pickup(this);
    //    }
    //}

    //public void RemoveScore(int am)
    //{
    //    score = score - am;
    //}

    //public int GetNumber()
    //{
    //    return score;
    //}

    enum Direction { Left, Right };

    public int basketValue;
    public bool isMovingBehind;

    void Spawn()
    {

    }

    /// <summary>
    /// Moves the basket towards the right direction.
    /// </summary>
    /// <param name="direction">Left or Right</param>
    void Move(Direction direction)
    {
        if (direction == Direction.Left)
        {
            
        }
        else if (direction == Direction.Right)
        {

        }
    }

    /// <summary>
    /// Called when an apple falls in the basket.
    /// The points of the apple will be substracted from the value of the basket.
    /// If the basketValue is smaller or equals to 0, a new value will be created
    /// and the score of the player will be updated.
    /// </summary>
    /// <param name="apple">The apple that fell into the basket</param>
    void CatchApple(Apple apple)
    {
        //int value is apple.getValue
        //Mand - apple
        //If mand <= 0, new mand
        //Update player score
    }

}
