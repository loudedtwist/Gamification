using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Networking;
using Parse;

public class TetrisGameScore : NetworkBehaviour {


    public struct TetrisScoreData
    {
        public int score;
        public string playerName;
        public string playerId;
        public int team;
    }; 

    public class ScoresSync : SyncListStruct<TetrisScoreData>{} 
    public ScoresSync gameScores = new ScoresSync();   

    public void Start()
    {   
        gameScores.Callback = GameScoresChanged;  
    } 

    void GameScoresChanged(ScoresSync.Operation op , int itemIndex){ 
        string shortMessage = "Tetris score list changed: " + op +"\n";
        Debug.LogError(shortMessage);
    }   

    public void AddTetrisScore(TetrisScoreData newAnswer){
        gameScores.Add(newAnswer); 
    } 
}
