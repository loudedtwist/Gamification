using System.Collections;
using System.Collections.Generic;
using Parse;
using UnityEngine;
using UnityEngine.Networking;

public class TeamPlayer : NetworkBehaviour
{
    public Team myTeam;

    public int myScore;

    [SerializeField] private Question questionManager;

    [SerializeField] private TeamManager teamManager;

    [Command]
    public void CmdAddAnswer(Question.Answer answer)
    {
        questionManager.AddAnswer(answer);
    }

    public void IncrementScore()
    {
        if (!isLocalPlayer) return;
        Debug.LogAssertion("MyScore changed " + myScore + 1 );
        myScore++;
        var user = ParseUser.CurrentUser;
        user["score"] = myScore;
        user.SaveAsync()
            .ContinueWith(task =>
            {
                if (task.IsCompleted && (task.IsFaulted || task.IsCanceled))
                {
                    GuiManager.Instance.message.For(2).ShowOnMainThread("Cant add +1 to score");
                    Debug.LogError("Cant save additional user data");
                }
                else
                {
                    GuiManager.Instance.message.For(2).ShowOnMainThread("Score +1 :)");
                    Debug.LogError("Additional user data saved");
                }
            });
    }

    public override void OnStartLocalPlayer()
    {
        GetPlayerScoreFromDb();
    }

    void OnEnable()
    {
        GameObject myObj = GameObject.FindGameObjectWithTag("Question");
        questionManager = myObj.GetComponent<Question>();
    }

    private void GetPlayerScoreFromDb()
    {
        myScore = ParseUser.CurrentUser.Get<int>("score");
    }

    void Start()
    {
        teamManager = GameObject.FindGameObjectWithTag("TeamManagerTag").GetComponent<TeamManager>();
        myTeam = teamManager.SignUpPlayerToTeam(this);
        if (myTeam == null)
        {
            //TODO React to full room -> show htw map , 
            Debug.LogError("Can't join the lobby, the room is full");
        }
    }

    private void OnDestroy()
    {
        teamManager.UnsignPlayerFromTeam(this);
    }
}