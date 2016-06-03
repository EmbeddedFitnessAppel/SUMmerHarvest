using UnityEngine;
using System.Collections;

public class Basket : Player {

    public GameObject playerBasket;

    public int minBasketValue;
    public int maxBasketValue;

    public int basketValue;
    public bool isMovingBehind;

    public int speed = 5;

    /// <summary>
    /// Moves the basket towards the right direction.
    /// </summary>
    /// <param name="direction">Left or Right</param>
    public override void Move(Direction direction)
    {
        if (direction == Direction.Left)
        {
            //playerBasket move left
            Debug.Log("Links");
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (direction == Direction.Right)
        {
            //playerBasket move right
            Debug.Log("Rechts");
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }

    /// <summary>
    /// Called when an apple falls in the basket.
    /// The points of the apple will be substracted from the value of the basket.
    /// If the basketValue is smaller or equals to 0, a new value will be created
    /// and the score of the player will be updated.
    /// </summary>
    /// <param name="apple">The apple that fell into the basket</param>
    public void CatchApple(Apple apple)
    {
        //int value is apple.getValue
        //Mand - apple
        //If mand <= 0, new mand
        //Update player score
    }

    /// <summary>
    /// Creates a new value for the basket.
    /// </summary>
    /// <param name="minValue">The minimum number of the new basketValue</param>
    /// <param name="maxValue">The maximum number of the new basketValue</param>
    public void ResetBasketValue(int minValue, int maxValue)
    {
        basketValue = Random.Range(minValue, maxValue);
    }
}
