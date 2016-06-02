using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team {
    public string Name { get; private set; }
    public Color Color { get; private set; }

    private Player[] players;

    public Team(string name, Color color, Player[] players) {
        this.Name = name;
        this.Color = color;
        this.players = players;
    }
}
