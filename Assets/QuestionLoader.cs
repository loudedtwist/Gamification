using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse;

public class QuestionLoader : MonoBehaviour {
    public Text questionLabel;
    public Text []answerLabels;  
    string question = "nope -_-";
    string answer1 = "-_-";
    string answer2 = "-_-";
    string answer3 = "-_-"; 


	// Use this for initialization
	void OnEnable () {
        var query = ParseObject.GetQuery("Quiz")
            .FindAsync().ContinueWith(t =>
            {
                IEnumerable<ParseObject> results = t.Result;
            });
        
        var query2 = ParseObject.GetQuery("Quiz");
        query2.FirstAsync().ContinueWith(t =>
            {
                ParseObject obj = t.Result;
                question = obj.Get<string>("question");
                answer1 = obj.Get<string>("answer");
                answer2 = obj.Get<string>("wrongAnswer1");
                answer3 = obj.Get<string>("wrongAnswer2");
            });
        
	}
	
	// Update is called once per frame
	void Update () {
        questionLabel.text = question;
        answerLabels[0].text = answer1;
        answerLabels[1].text = answer2;
        answerLabels[2].text = answer3;
	}
}
