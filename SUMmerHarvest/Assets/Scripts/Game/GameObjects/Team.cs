using Assets.Scripts.Game.GameObjects;
using UnityEngine;

public class Team
{
    private readonly Player[] players;

    public Team(string name, Color color, Player[] players)
    {
        Name = name;
        Color = color;
        this.players = players;

        foreach (var p in players)
        {
            p.Team = this;
        }
    }

    public string Name { get; private set; }
    public Color Color { get; private set; }

    public int Score
    {
        get
        {
            var score = 0;
            foreach (var player in players)
            {
                var basket = player as Basket;
                if (basket != null)
                {
                    score += basket.Score;
                }
            }
            return score;
        }
    }
}