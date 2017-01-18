using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    private string teamName;

    List<Player> players;

    public int maxPlayerCount;

    public int minPlayerCount = 2;

    public Color teamColor;

    public Team()
    {
        players = new List<Player>();
        teamColor = new Color(0.6f,0.6f,0.8f);
    }

    public void AddPlayerToTeam(Player player)
    {
        if (player != null)
            players.Add(player);
    }

    public void DeletePlayerFromTeam(Player player)
    {
        foreach (var p in players)
        {
            if (p.GetPlayerId() == player.GetPlayerId())
                players.Remove(p);
        }
    }

}