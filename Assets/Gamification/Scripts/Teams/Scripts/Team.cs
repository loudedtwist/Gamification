using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    private string teamName;

    public int teamNr;

    [SerializeField] private bool isReady;

    public bool IsReady
    {
        get { return maxPlayerCount == players.Count; }
        private set { }
    }

    List<TeamPlayer> players;

    public List<TeamPlayer> Players
    {
        get { return players; }
    }

    public int maxPlayerCount;
    public int minPlayerCount = 2;

    public Color teamColor;
    public Color teamColorConnected;
    public Color teamColorDisconnected;

    public void Start()
    {
        players = new List<TeamPlayer>();
        teamColor = new Color(0.6f, 0.6f, 0.8f);
    }

    public void AddPlayerToTeam(TeamPlayer player)
    {
        if (player != null)
            players.Add(player);
    }

    public void DeletePlayerFromTeam(TeamPlayer player)
    {
        foreach (var p in players)
        {
            if (p == player)
                players.Remove(p);
        }
    }
}