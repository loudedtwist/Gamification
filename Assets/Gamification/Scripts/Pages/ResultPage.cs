using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ResultPage : MonoBehaviour
{

    public Text teamALabel;

    public Text teamBLabel;

    public Text deinePunkteLabel;

    public TeamManager teamManager;
    private Question questionManager;

    public void OnEnable()
    {

        GameObject myObj = GameObject.FindGameObjectWithTag("Question");
        if (myObj == null)
        {
            Debug.LogError("NO QIESTION OBJECT WITH TAG Question");
            return;
        }

        questionManager = myObj.GetComponent<Question>();
        int scoreA = 0;
        int scoreB = 0;

        foreach (var answer in questionManager.answers)
        {
            if (answer.team == teamManager.teamA.teamNr && answer.correct)
                scoreA++;
            if (answer.team == teamManager.teamB.teamNr && answer.correct)
                scoreB++;
        }

        string team1Text = teamManager.localPlayer.myTeam.teamNr == teamManager.teamA.teamNr ? "My Team" : "Gegner-Team";
        teamALabel.text = team1Text + ": " + scoreA + " Punkte";
        string team2Text = teamManager.localPlayer.myTeam.teamNr == teamManager.teamB.teamNr ? "My Team" : "Gegner-Team";
        teamBLabel.text = team2Text + ": " + scoreB + " Punkte";

        Team winTeam = DecideWhoWins(scoreA, scoreB);
        GiveRewardIfNeeded(winTeam);

        deinePunkteLabel.text = "Deine Punkte: " + teamManager.localPlayer.myScore;


        teamManager.ClearTeams();
    }

    private void GiveRewardIfNeeded(Team team)
    {
        if (team == null)
        {
            Debug.LogError("WIN TEEM WAS NULL");
            return;
        }
        foreach (var teamPlayer in team.Players)
        {
            teamPlayer.AddRandomPowerUp();
        }
    }

    private Team DecideWhoWins(int scoreA, int scoreB)
    {
        if (scoreA > scoreB)
        {
            return teamManager.teamA;
        }
        else if (scoreB > scoreA)
        {
            return teamManager.teamB;
        }
        else return null;
    }

    private int getCalculatedScoreFromTeam(Team team)
    {
        return team.Players.Sum(teamPlayer => teamPlayer.myScore);
    }

}
