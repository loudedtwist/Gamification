using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    //public List<Team> teams;

    public Team teamA;

    public Team teamB;

    public TeamManager()
    {
        teamA = new Team();
        teamB = new Team();
    }

    public Team GetTeamA()
    {
        return teamA;
    }

    public Team GetTeamB()
    {
        return teamB;
    }
}
