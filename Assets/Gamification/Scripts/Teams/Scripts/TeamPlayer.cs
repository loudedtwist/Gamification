using System;
using System.Collections;
using System.Collections.Generic;
using Parse;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class TeamPlayer : NetworkBehaviour
{
    public Team myTeam;
    private ParseUser user;

    public int myScore;

    private int currentMirrors, currentBooms, currentNoizes;

    [SerializeField] private Question questionManager;

    [SerializeField] private TeamManager teamManager;

    void Start()
    {
        user = ParseUser.CurrentUser;
        teamManager = GameObject.FindGameObjectWithTag("TeamManagerTag").GetComponent<TeamManager>();
        myTeam = teamManager.SignUpPlayerToTeam(this);
        if (myTeam == null)
        {
            //TODO React to full room -> show htw map ,
            Debug.LogError("Can't join the lobby, the room is full");
        }

    }

    void OnEnable()
    {
        GameObject myObj = GameObject.FindGameObjectWithTag("Question");
        questionManager = myObj.GetComponent<Question>();
    }

    [Command]
    public void CmdAddAnswer(Question.Answer answer)
    {
        questionManager.AddAnswer(answer);
    }

    public void IncrementScore()
    {
        if (!isLocalPlayer) return;
        Debug.LogAssertion("MyScore changed " + myScore + 1);
        myScore++;
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
        Debug.Log("STARTED PLAYER!! ! ! ");
        GetPlayerScoreFromDb();
    }


    private void GetPlayerScoreFromDb()
    {
        myScore = ParseUser.CurrentUser.Get<int>("score");
    }

    public override void OnNetworkDestroy()
    {
        Debug.LogAssertion("OnNetworkDestroy in TEAMPLAYER");
        GuiManager.Instance.message.For(2).Show("Disconnected");
        GuiManager.Instance.ShowUserHomePage();
        teamManager.UnsignPlayerFromTeam(this);
    }

    public void AddRandomPowerUp()
    {
        if (!isLocalPlayer) return;
        var powerUp = Random.Range(0, 3);
        switch (powerUp)
        {
            case 0:
                var currentMirrors = CurrentMirrors();
                user["mirrors"] = currentMirrors + 1;
                Debug.LogAssertion("MIRROR CHOOSED");
                break;
            case 1:
                var currentNoiz = CurrentNoiz();
                user["noiz"] = currentNoiz + 1;
                Debug.LogAssertion("NOIZ CHOOSED");
                break;
            case 2:
                var currentBooms = CurrentBooms();
                user["booms"] = currentBooms + 1;
                Debug.LogAssertion("BOOM CHOOSED");
                break;
            default:
                break;
        }
        user.SaveAsync()
            .ContinueWith(task =>
            {
                if (task.IsCompleted && (task.IsFaulted || task.IsCanceled))
                {
                    GuiManager.Instance.message.For(2).ShowOnMainThread("Cant add +1 powerUp");
                    Debug.LogError("Cant add powerup");
                    Debug.LogError("Cant add powerup");
                    Debug.LogError("Cant add powerup");
                }
                else
                {
                    GuiManager.Instance.message.For(2).ShowOnMainThread("PowerUp +1 :)");
                    Debug.LogError("Added power up");
                    Debug.LogError("Added power up");
                    Debug.LogError("Added power up");
                }
            });
    }

    public void DecrementPowerUp(PowerTypes powerType, Action doIfHasPower)
    {
        if (!isLocalPlayer) return;
        switch (powerType)
        {
            case PowerTypes.Mirror:
                var currentMirrors = CurrentMirrors();
                if (currentMirrors == 0) return;
                user["mirrors"] = currentMirrors - 1;
                break;
            case PowerTypes.Noiz:
                var currentNoiz = CurrentNoiz();
                if (currentNoiz == 0) return;
                user["noiz"] = currentNoiz - 1;
                break;
            case PowerTypes.Boom:
                var currentBooms = CurrentBooms();
                if (currentBooms == 0) return;
                user["booms"] = currentBooms - 1;
                break;
            default:
                break;
        }
        user.SaveAsync()
            .ContinueWith(task =>
            {
                if (task.IsCompleted && (task.IsFaulted || task.IsCanceled))
                {
                    GuiManager.Instance.message.For(2).ShowOnMainThread("Cant add -1 powerUp");
                    Debug.LogError("Cant substract powerup");
                }
                else
                {
                    doIfHasPower.Invoke();
                    GuiManager.Instance.message.For(2).ShowOnMainThread("PowerUp -1 :)");
                    Debug.LogError("Substacted power up");
                }
            });
    }

    private int CurrentBooms()
    {
        return user.Get<int>("booms");
    }

    private int CurrentNoiz()
    {
        return user.Get<int>("noiz");
    }

    private int CurrentMirrors()
    {
        return user.Get<int>("mirrors");
    }
}