using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{

    public Team teamA;

    public Team teamB;

    public UsersConnection usersConnection;

    public void Start()
    {

    }

    void Update()
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

    public void UnsignPlayerFromTeam(TeamPlayer teamPlayer)
    {
        teamPlayer.myTeam.DeletePlayerFromTeam(teamPlayer);
    }
}
