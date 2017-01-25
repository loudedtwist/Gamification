using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Parse;



public class Question : NetworkBehaviour
{

    public struct Answer
    {
        public string playerId;
        public string playerName;
        public string time;
        public bool correct;
        public int team;
    };

    public class AnswersSync : SyncListStruct<Answer>{} 
    public AnswersSync answers = new AnswersSync();  
    public SyncListInt questionsSynced = new SyncListInt();

    public void Start()
    {  
        ChangeMyQuestion();
        answers.Callback = AnswerListChanged; 
        questionsSynced.Callback = QuestionListChanged;
    }


    public string toStringAnswer(Answer a){
        return "{ "+ "\n"
            +"PId: "+a.playerId + "\n"
            +"PName: "+a.playerName + "\n"
            +"Time: "+a.time + "\n"
            +"AnswerCorrect: "+a.correct + "\n"
            +"team: "+a.team + "\n"
            +"}\n";
    }

    void AnswerListChanged(AnswersSync.Operation op , int itemIndex){
        string message = "Answer list changed: " + op + "\n";
        foreach(var a in answers){
            message += toStringAnswer(a) + "\n";
        }
        Debug.LogError(message);
    } 

    void QuestionListChanged(SyncListInt.Operation op , int itemIndex){
        string message = "Quest list changed: " + op + " > " +questionsSynced[itemIndex] + "\n"; 
        Debug.LogError(message);
    }  

    public void ChangeMyQuestion()
    {
        if(!isServer) return;
        StartCoroutine (GetQuestionNrAsync()); 
    }

    private IEnumerator GetQuestionNrAsync()
    {
        var countTask = ParseObject.GetQuery("Quiz").CountAsync();
        while(!countTask.IsCompleted) yield return null;
        int anz =  countTask.Result;

        int []questions = new int[6];
        FillWithRandomUniqueNumbers(questions, anz);

        if(questionsSynced.Count > 0) questionsSynced.Clear();
        for(int i = 0; i < questions.Length; i++){
            questionsSynced.Add(questions[i]);
        }
    }

    public void AddAnswer(Answer newAnswer){
        answers.Add(newAnswer); 
    }

    void FillWithRandomUniqueNumbers(int []arrayToFill, int maxExclusive){ 
        int random;
        arrayToFill.FillWith(-1);

        for(int i= 0; i < arrayToFill.Length;i++){
            do
            {
                random= Random.Range(0, maxExclusive);
            } while(arrayToFill.Contains(random)); 
            arrayToFill[i] = random;
        }
    }
}
