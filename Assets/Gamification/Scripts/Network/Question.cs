using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



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

    [SyncVar(hook = "OnChangeQuestionNr")]
    public int questionNr = -1; 
 
    public void Start()
    {
        Invoke("ChangeMyQuestion", 3.0f);
        answers.Callback = AnswerListChanged; 
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

    void AnswerListChanged(UnityEngine.Networking.SyncListStruct<Answer>.Operation op , int itemIndex){
        string message = "Answer list changed: " + op + "\n";
        foreach(var a in answers){
            message += toStringAnswer(a) + "\n";
        }
        Debug.LogError(message);
    } 

    public void OnChangeQuestionNr(int newNr)
    {
        Debug.LogError("CHANGED QUESTION NR ");
        questionNr = newNr;
    }


    public void ChangeMyQuestion()
    {
        if(!isServer) return;   
        questionNr = Random.Range(0, 6);
    } 

    public void AddAnswer(Answer newAnswer){
        answers.Add(newAnswer); 
    }
}
