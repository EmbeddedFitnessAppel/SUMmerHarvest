using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team {
    public string Name { get; private set; }
    public Color Color { get; private set; }

    private Player[] players;

    public int Score {
        get {
            int score = 0;
            foreach (Player player in this.players) {
                Basket basket = player as Basket;
                if (basket != null) {
                    score += basket.Score;
                }
            }
            return score;
        }
    }

    public Team(string name, Color color, Player[] players) {
        this.Name = name;
        this.Color = color;
        this.players = players;
    }
}
