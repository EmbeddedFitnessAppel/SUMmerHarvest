using UnityEngine;
using System.Collections;

public class Basket : Player {

    public GameObject playerBasket;

    public int MinBasketValue;
    public int MaxBasketValue;

    public int BasketValue;
    public bool IsMovingBehind;

    public int Score;

    public void Update()
    {
        transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed, 0));
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
        BasketValue = BasketValue - apple.ScoreValue;
        if (BasketValue == 0)
        {
            Score++;
            ResetBasketValue(MinBasketValue, MaxBasketValue);
        }
        else if (BasketValue < 0)
        {
            ResetBasketValue(MinBasketValue, MaxBasketValue);
        }
    }

    /// <summary>
    /// Creates a new value for the basket.
    /// </summary>
    /// <param name="minValue">The minimum number of the new basketValue</param>
    /// <param name="maxValue">The maximum number of the new basketValue</param>
    public void ResetBasketValue(int minValue, int maxValue)
    {
        BasketValue = Random.Range(minValue, maxValue);
    }
}
