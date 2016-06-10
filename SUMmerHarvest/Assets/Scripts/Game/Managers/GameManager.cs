using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
    private List<Team> teams = new List<Team>();

    private bool gameStarted = false;
    private float timeLeft = 0f;
    public float RoundTime;

    [SerializeField]
    private GameObject appleManager;

    [Header("Prefabs")]
    [SerializeField]
    private Basket basketPrefab;
    [SerializeField]
    private Monkey monkeyPrefab;


    [Header("Team blu")]
    [SerializeField]
    private Basket bluBasket;
    [SerializeField]
    private Monkey bluMonkey;

    [Header("Team red")]
    [SerializeField]
    private Basket redBasket;
    [SerializeField]
    private Monkey redMonkey;


    void Start() {
        // TODO: Instantiate baskets and monkeys and properly initialize them.
        { // Team RED
            //Basket b = Instantiate<Basket>(basketPrefab);
            //Monkey m = Instantiate<Monkey>(monkeyPrefab);

            this.teams.Add(new Team("Team_BLU", Color.blue, new Player[] { bluBasket, bluMonkey }));
        }

        { // Team BLU
            //Basket b = Instantiate<Basket>(basketPrefab);
            //Monkey m = Instantiate<Monkey>(monkeyPrefab);

            this.teams.Add(new Team("Team_RED", Color.red, new Player[] { redBasket, redMonkey }));
        }

        this.StartGame(RoundTime);
    }

    void Update() {
        if (gameStarted) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                EndGame();
            } else {
                UIManager.Instance.SetCountdownText(timeLeft);
            }
        }


    }


    public void StartGame(float gameDuration) {
        if (gameStarted == true) {
            throw new System.InvalidOperationException("StartGame was called while the game was already started.");
        }

        timeLeft = gameDuration;

        gameStarted = true;
    }

    public void EndGame() {
        // Stop apples from spawning.
        appleManager.SetActive(false);

        gameStarted = false;
        UIManager.Instance.ShowEndgamePanel(this.teams);
    }
}
