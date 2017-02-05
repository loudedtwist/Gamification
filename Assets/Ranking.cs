using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse;
public class Ranking : MonoBehaviour {
    public Text rankingText;

    void OnEnable(){
        rankingText.text="Ranking Top 10:\n";
        StartCoroutine(GetRanking());
    }

    IEnumerator GetRanking(){ 
        var task = ParseUser.Query
            .OrderByDescending("score")
            .Limit(10)
            .FindAsync();
        while(!task.IsCompleted) yield return null;

        IEnumerable<ParseUser> users = task.Result;
        foreach(var user in users){
            rankingText.text = rankingText.text + "\n" + user.Get<string>("username") + ": " + user.Get<int>("score");
        }


    }
}
