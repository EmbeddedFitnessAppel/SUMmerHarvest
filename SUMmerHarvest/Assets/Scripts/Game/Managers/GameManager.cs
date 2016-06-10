using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
    private List<Team> teams = new List<Team>();

    private bool gameStarted = false;
    private float timeLeft = 0f;

    [SerializeField]
    private GameObject appleManager;


    void Start() {
        this.teams.Add(new Team("Team_RED", Color.blue, new Player[] { new Basket(), new Monkey() }));
        this.teams.Add(new Team("Team_BLU", Color.red, new Player[] { new Basket(), new Monkey() }));

        this.StartGame(5f);
    }

    void Update() {
        if (gameStarted) {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                EndGame();
            }
            UIManager.Instance.SetCountdownText(timeLeft);
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
