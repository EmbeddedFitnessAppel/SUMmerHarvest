using UnityEngine;
using System.Collections;

public class Basket : Player {

    public GameObject playerBasket;

    public int minBasketValue;
    public int maxBasketValue;

    public int basketValue;
    public bool isMovingBehind;

    public int score;

    /// <summary>
    /// Moves the basket towards the right direction.
    /// </summary>
    /// <param name="direction">Left or Right</param>
    public override void Move(Direction direction, float speed)
    {
        if (direction == Direction.Left)
        {
            //playerBasket move left
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (direction == Direction.Right)
        {
            //playerBasket move right
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
        basketValue = basketValue - apple.scoreValue;
        if (basketValue == 0)
        {
            score++;
            ResetBasketValue(minBasketValue, maxBasketValue);
        }
        else if (basketValue < 0)
        {
            ResetBasketValue(minBasketValue, maxBasketValue);
        }
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
