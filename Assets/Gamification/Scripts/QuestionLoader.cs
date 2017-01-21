using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse;

public class QuestionLoader : MonoBehaviour {
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
 
    void OnEnable () { 
        rightAnswerIndex = -1;
        answers = new string[3];
        randoms = new int[3]; 

        GetQuestionNr();
        FillWithRandomUniqueNumbers(randoms);
        answerButtons.ChangeStateOfButtons(false);  
        StartCoroutine (GetQuestionAndUpdateUi()); 
        //execution of code after last line will continue immediately
	}

    void GetQuestionNr()
    {
        GameObject myObj = GameObject.FindGameObjectWithTag("Question");
        if (myObj != null)
        {
            var question = myObj.GetComponent<Question>();
            if (question == null) return;
            questionNr = question.questionNr;
        }
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
        ParseObject obj = asyncTask.Result;
        question = obj.Get<string>("question"); 

        string []answersFromServer = new string[3];
        answersFromServer[0] = obj.Get<string>("answer");
        answersFromServer[1] = obj.Get<string>("wrongAnswer1");
        answersFromServer[2] = obj.Get<string>("wrongAnswer2");

        RandomizeAnswerButtons(answersFromServer);  
        UpdateQuestionUI(question, answers);
    }

    public void UpdateQuestionUI(string question, string []answers){ 
        questionLabel.text = question;
        for(int i = 0; i < 3 ; i++){
            answerLabels[i].text = answers[i];  
        } 
        answerButtons.ChangeStateOfButtons(true);
    }

    public void OnAnswerClicked(int buttonIndex){
        Debug.LogAssertion("BUTTON INDEX: " + buttonIndex + " RIGHT ANSWER: " + rightAnswerIndex);
        if (buttonIndex == rightAnswerIndex)
            trueFalse.color = Color.green;
        else 
            trueFalse.color = Color.red;
    } 
}
