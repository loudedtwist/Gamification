using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse;
using Random = UnityEngine.Random;

public class QuestionLoader : MonoBehaviour {
    public int round = -1;

    [SerializeField]
    private TeamManager teamManager;

    public Text questionLabel;
    public Text []answerLabels;  
    public Button []answerButtons;  

    private string question;
    private string []answers;  
    private int []randoms;
    public int rightAnswerIndex;
    private int questionNr;

    public Image trueFalse;
    public Question questionManager;
    public LoadingProgress progressBar;
    public GameManager gameManager;

    public Color trueAnswerColor;
    public Color falseAnswerColor;
    public Color falseAnswerStartColor;

    public void NewGame()
    {
        round = -1;
    }

    void OnEnable () { 
        round++;

        GameObject myObj = GameObject.FindGameObjectWithTag("Question");
        if (myObj != null) questionManager = myObj.GetComponent<Question>();

        rightAnswerIndex = -1;
        answers = new string[3];
        randoms = new int[3]; 

        GetQuestionNr();
        FillWithRandomUniqueNumbers(randoms);
        answerButtons.Interactable(false);  
        StartCoroutine (GetQuestionAndUpdateUi()); 
        progressBar.StartLoadingAnimation(gameManager.gameDuration);
        //execution of code after last line will continue immediately
	}

    void GetQuestionNr()
    {
        if (questionManager == null)
        {
            Debug.LogError("QuestionManager is null");
            return;
        }
        questionNr = questionManager.questionsSynced[round];
    }

    void FillWithRandomUniqueNumbers(int []arrayToFill){ 
        int random;
        arrayToFill.FillWith(-1);

        for(int i= 0; i < arrayToFill.Length;i++){
            do
            {
                random= Random.Range(0, arrayToFill.Length);
            } while(arrayToFill.Contains(random)); 
            arrayToFill[i] = random;
        }
    }

    void RandomizeAnswerButtons(string[] answersFromServer)
    { 
        rightAnswerIndex = randoms[0];

        for(int i = 0; i < 3 ; i++){
            int randomIndex = randoms[i];
            answers[randomIndex] = answersFromServer[i]; 
        } 
    }

    IEnumerator GetQuestionAndUpdateUi () { 
        
        var query = ParseObject.GetQuery("Quiz");
        var asyncTask = query.Skip(questionNr).FirstAsync();  
        while(!asyncTask.IsCompleted) yield return null;

        //after task is ready it will execute  code on main thread.
        ParseObject obj;
        try
        {
            obj = asyncTask.Result;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            StartCoroutine (GetQuestionAndUpdateUi()); 
            throw;
        }

        question = obj.Get<string>("question"); 

        string []answersFromServer = new string[3];
        answersFromServer[0] = obj.Get<string>("answer");
        answersFromServer[1] = obj.Get<string>("wrongAnswer1");
        answersFromServer[2] = obj.Get<string>("wrongAnswer2");

        trueFalse.color = falseAnswerStartColor;
        RandomizeAnswerButtons(answersFromServer);  
        UpdateQuestionUI(question, answers); 
    }

    public void UpdateQuestionUI(string question, string []answers){ 
        questionLabel.text = question;
        for(int i = 0; i < 3 ; i++){
            answerLabels[i].text = answers[i];  
        } 
        answerButtons.Interactable(true);
    }

    public void OnAnswerClicked(int buttonIndex){
        bool answerCorrect = false;
        Debug.LogAssertion("BUTTON INDEX: " + buttonIndex + " RIGHT ANSWER: " + rightAnswerIndex);

        if (buttonIndex == rightAnswerIndex)
        {
            teamManager.localPlayer.IncrementScore();
            trueFalse.color = trueAnswerColor;
            answerCorrect = true;
        }
        else
        { 
            trueFalse.color = falseAnswerColor;
            answerCorrect = false;
        }

        answerButtons[buttonIndex].GetComponent<EWZButtonState>().AnswerHit(answerCorrect);

        var answer = new Question.Answer();
        answer.team = teamManager.localPlayer.myTeam.teamNr;
        answer.playerName = ParseUser.CurrentUser.Username;
        answer.playerId = ParseUser.CurrentUser.ObjectId;
        answer.time = System.DateTime.Now.ToShortTimeString();
        answer.correct = answerCorrect;
        //questionManager.AddAnswer(answer);

        teamManager.localPlayer.CmdAddAnswer(answer);

        answerButtons.Interactable(false);
    } 
}
