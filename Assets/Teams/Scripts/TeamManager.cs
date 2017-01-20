﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{ 
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
        if (teamA.Players.Count > teamB.Players.Count)
        {
            if(teamB.Players.Count == 0) 
            teamB.Players.Add(player);
            if (teamB.IsReady) return null;
            else return teamB;
        }
        else
        {
            teamA.Players.Add(player);
            if (teamA.IsReady) return null;
            else return teamA;
        }

        if (teamA.IsReady && teamB.IsReady)
            gameManager.StartGame();
    }


    public void UnsignPlayerFromTeam(TeamPlayer teamPlayer)
    {
        teamPlayer.myTeam.DeletePlayerFromTeam(teamPlayer);
    }
}