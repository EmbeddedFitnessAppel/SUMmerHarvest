using UnityEngine;

public class Basket : Player
{
    public GameObject playerBasket;

    public int MinBasketValue;
    public int MaxBasketValue;

    public int BasketValue;
    public bool IsMovingBehind;

    public int Score;
    public int playerNumber;

    //Thes variables are for dodging other players.
    //They are used in the Bumper.cs.
    public bool isExtended;
    public string extendDirection;

    public void Update()
    {
        switch (playerNumber)
        {
            case 1:
                transform.Translate(new Vector3(Input.GetAxisRaw("Player1Horizontal") * Time.deltaTime * Speed, 0));
                break;
            case 2:
                transform.Translate(new Vector3(Input.GetAxisRaw("Player2Horizontal") * Time.deltaTime * Speed, 0));
                break;
            default:
                Debug.Log("Invalid Player number!");
                break;
        }
    }

    /// <summary>
    ///     Called when an apple falls in the basket.
    ///     The points of the apple will be substracted from the value of the basket.
    ///     If the basketValue is smaller or equals to 0, a new value will be created
    ///     and the score of the player will be updated.
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
    ///     Creates a new value for the basket.
    /// </summary>
    /// <param name="minValue">The minimum number of the new basketValue</param>
    /// <param name="maxValue">The maximum number of the new basketValue</param>
    public void ResetBasketValue(int minValue, int maxValue)
    {
        BasketValue = Random.Range(minValue, maxValue);
    }

    /// <summary>
    ///     Moves the basket forward, used for dodging other baskets.
    /// </summary>
    public void DodgeForward()
    {
        transform.Translate(new Vector3(0, 0, -1.8f));
    }

    /// <summary>
    ///     Moves the basket backward, used for dodging other baskets.
    /// </summary>
    public void DodgeBackward()
    {
        transform.Translate(new Vector3(0, 0, 1.8f));
    }
}