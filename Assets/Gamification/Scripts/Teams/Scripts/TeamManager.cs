using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    public TeamPlayer localPlayer;

    public Team teamA;
    public Team teamB;

    public GameManager gameManager;

    public UsersConnection usersConnection;

    void Start()
    {
        InvokeRepeating("UpdateInfos", 1, 1);
    }

    void OnDestroy()
    {
        CancelInvoke("UpdateInfos");
    }

    public void UpdateInfos()
    {
        usersConnection.ShowUsersConnected(teamA);
        usersConnection.ShowUsersConnected(teamB);
    }

    public Team GetTeamA()
    {
        return teamA;
    }

    public Team GetTeamB()
    {
        return teamB;
    }

    public Team SignUpPlayerToTeam(TeamPlayer player)
    {
        SaveIfLocalPlayer(player);

        var playerTeam = AssignToTeam(player);

        StartGameIfTeamsAreReady();

        return playerTeam;
    }

    void SaveIfLocalPlayer(TeamPlayer player)
    {
        if (player.isLocalPlayer)
            localPlayer = player;
    }

    Team AssignToTeam(TeamPlayer player)
    {
        if (teamA.Players.Count > teamB.Players.Count)
        {
            teamB.Players.Add(player);
            return teamB;
        }
        else
        {
            teamA.Players.Add(player);
            return teamA;
        }
    }

    void StartGameIfTeamsAreReady()
    {
        if (teamA.IsReady && teamB.IsReady)
            gameManager.StartGame();
    }

    public void UnsignPlayerFromTeam(TeamPlayer teamPlayer)
    {
        teamPlayer.myTeam.DeletePlayerFromTeam(teamPlayer);
    }
}