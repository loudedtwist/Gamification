using System;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;
using UnityEngine.UI;
using Parse;

public class GameManager : MonoBehaviour
{
    public TetrisRound tetris; 
    TetrisGameScore tetrisScore;
    public Text readyGoBanner;
    public string readyText = "Game starts in ";
    public float secondsUntilStartVal = 3.0f;
    bool wasTetrisPlayed = false;

    public float roundDuration = 20.0f;
    public float tetrisDuration = 20.0f;
    public int roundNr = 0;
    public TeamManager teams;
    public HostGame gameConnection;

    //change rounds in NetworkObject index generator
    public int roundsNumber = 6;

    [SerializeField] private int roundsRemains;
    public QuestionLoader questionLoader;

    private float secondsUntilStartCounter;

    public void StartGame()
    {

        tetrisScore = GameObject.FindGameObjectWithTag("Question").GetComponent<TetrisGameScore>();

        var startTime = secondsUntilStartVal + 1.0f;
        var timeUntilTetris = roundsNumber / 2 * roundDuration;
        roundsRemains = roundsNumber/2;
        roundNr = 0;

        secondsUntilStartCounter = secondsUntilStartVal;
        ShowBanner(true);

        questionLoader.NewGame();
        Invoke("ChangeToQuizPage", startTime);
        InvokeRepeating("StartRound", startTime, roundDuration);
        //Invoke("StartTetris", startTime + timeUntilTetris);
        //InvokeRepeating("StartRound", startTime + timeUntilTetris + tetrisDuration + 10.0f, gameDuration); 
        Invoke("FinishGame", startTime + roundDuration * roundsNumber + tetrisDuration);
        InvokeRepeating("DecrementTimeToStart", 0.0f, 1.0f);
    }
    public void StopGame(){ 
        CancelInvoke("ChangeToQuizPage");
        CancelInvoke("StartRound"); 
        CancelInvoke("FinishGame");
        CancelInvoke("DecrementTimeToStart");
    }

    public void DecrementTimeToStart()
    {
        if (secondsUntilStartCounter < 1)
        {
            CancelInvoke("DecrementTimeToStart");
            readyGoBanner.text = "GO";
            return;
        }
        readyGoBanner.text = readyText + secondsUntilStartCounter;
        --secondsUntilStartCounter;
    }

    public void ChangeToQuizPage()
    {
        ShowBanner(false);
        GuiManager.Instance.ShowQuizPage();
    }

    public void FinishGame()
    {
        gameConnection.DropPreviousMatch();
        GuiManager.Instance.ShowQuizResultsPage();
        //go to score page
    }

    public void StartRound()
    {
        --roundsRemains;

        Debug.LogAssertion("##############");
        Debug.LogAssertion("ROUND: " + (++roundNr) + " #");
        Debug.LogAssertion("ROUNDS REMAINS: " + roundsRemains + " #");
        Debug.LogAssertion("############## ");
        GuiManager.Instance.ShowQuizPage();

        if (roundsRemains < 1)
        {
            Debug.LogAssertion("STOPPING ROUNDS INVOKE");
            CancelInvoke("StartRound");
            if (!wasTetrisPlayed)
            {
                wasTetrisPlayed = true;
                Invoke("StartTetris", roundDuration);
                Invoke("StopTetris", roundDuration + tetrisDuration);
            }
        }
    }

    void StartTetris(){
        questionLoader.ClearPage();
        GuiManager.Instance.HideAllPages(); 
        GuiManager.Instance.ShowTetrisPage(); 
    }

    void StopTetris(){ 

        SendTetrisScore();

        roundsRemains = roundsNumber/2;
        GuiManager.Instance.BackFromTetrisPage(); 
        InvokeRepeating("StartRound", 1.0f, roundDuration);
        Invoke("ShowWhoWonTetris", 2.0f);
    }

    void ShowWhoWonTetris(){

        TetrisGameScore.TetrisScoreData? myScore = null;
        foreach( var score in tetrisScore.gameScores){
            if (score.playerName == ParseUser.CurrentUser.Username)
                myScore = score;
        }

        if(myScore == null){
                
            Debug.LogError("TETRIS SCORE: Your score not found");
            GuiManager.Instance.message.For(4).Show("Tetris: score not found");
        }

        List<TetrisGameScore.TetrisScoreData> scoreList = new List<TetrisGameScore.TetrisScoreData>();
        foreach( var score in tetrisScore.gameScores){
            scoreList.Add(score);
        }

        bool won = false;
        string message; 
        string messageWon = "You won a Power up.";
        string messageLost = "You lost the game."; 

        scoreList.Sort((a, b) => { return a.score.CompareTo(b.score); });
        for( int i=0;i < scoreList.Count; i++){
            if (scoreList[i].playerName == ParseUser.CurrentUser.Username && i < scoreList.Count/2)
                won = true;
        }

        if(won){
            message = messageWon + " Score: " + myScore.Value.score;
        }else{ 
            message = messageLost + " Score: " + myScore.Value.score;
        }
        GuiManager.Instance.message.For(4).Show(message);
        
    }
    void SendTetrisScore()
    {
        var score = new TetrisGameScore.TetrisScoreData();
        score.team = teams.localPlayer.myTeam.teamNr;
        score.playerName = ParseUser.CurrentUser.Username;
        score.playerId = ParseUser.CurrentUser.ObjectId;
        score.score = TetrisRound.points;
        teams.localPlayer.CmdAddTetrisScore(score);
    }

    void ShowBanner(bool state)
    {
        readyGoBanner.gameObject.transform.parent.gameObject.SetActive(state);
    }
}