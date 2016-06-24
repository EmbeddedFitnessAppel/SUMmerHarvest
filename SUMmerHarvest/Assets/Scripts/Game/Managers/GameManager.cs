using System;
using System.Collections.Generic;
using Assets.Scripts.Game.GameObjects;
using Assets.Scripts.Game.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private readonly List<Team> teams = new List<Team>();

    private bool gameStarted;
    private float timeLeft;
    public float RoundTime;

    [SerializeField]
    private GameObject appleManager;

    [Header("Prefabs")]
    [SerializeField]
    private Basket basketPrefab;

    [SerializeField]
    private Monkey monkeyPrefab;


    [SerializeField]
    private GameObject MonkeyInRangeIndicator;

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


    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        // TODO: Instantiate baskets and monkeys and properly initialize them.
        {
            // Team RED
            //Basket b = Instantiate<Basket>(basketPrefab);
            //Monkey m = Instantiate<Monkey>(monkeyPrefab);

            teams.Add(new Team("Team_BLU", new Color(0.1f, 0.3f, 0.9f), new Player[] { bluBasket, bluMonkey }));
        }

        {
            // Team BLU
            //Basket b = Instantiate<Basket>(basketPrefab);
            //Monkey m = Instantiate<Monkey>(monkeyPrefab);

            teams.Add(new Team("Team_RED", new Color(0.9f, 0.2f, 0.2f), new Player[] { redBasket, redMonkey }));
        }

        UIManager.Instance.SetTeams(this.teams[0], this.teams[1]);

        StartGame(RoundTime);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name.IndexOf("Game", StringComparison.OrdinalIgnoreCase) < 0) return;

        if (gameStarted)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0)
            {
                EndGame();
            }
            else
            {
                UIManager.Instance.SetCountdownText(timeLeft);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }


    public void StartGame(float gameDuration)
    {
        if (gameStarted)
        {
            throw new InvalidOperationException("StartGame was called while the game was already started.");
        }

        timeLeft = gameDuration;

        gameStarted = true;
    }

    public void EndGame()
    {
        // Stop apples from spawning.
        appleManager.SetActive(false);

        gameStarted = false;
        UIManager.Instance.ShowEndgamePanel(teams);
    }

    /// <summary>
    /// Creates and returns a range indicator for monkeys
    /// </summary>
    /// <returns>The GameObject that references the specific UI part</returns>
    public GameObject CreateMonkeyInRangeIndicator()
    {
        GameObject m = GameObject.Instantiate(MonkeyInRangeIndicator);
        UIManager.Instance.PutInWorldCanvas(m);
        return m;
    }


}