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
            if (answer.team == 1 && answer.correct)
                scoreA++;
            if (answer.team == 2 && answer.correct)
                scoreB++;
        }


        teamALabel.text += scoreA + " Punkte";
        teamBLabel.text += scoreB + " punkte";
        deinePunkteLabel.text +=  teamManager.localPlayer.myScore;
    }

    private int getCalculatedScoreFromTeam(Team team)
    {
        return team.Players.Sum(teamPlayer => teamPlayer.myScore);
    }

}
