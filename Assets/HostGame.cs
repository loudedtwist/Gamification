using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using UnityEngine.Networking.Match;

public class HostGame : MonoBehaviour {
    
    [SerializeField]
    private uint roomSize = 6; 
    private string roomName;
    private NetworkManager networkManager;

    void Start(){
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null ){
            networkManager.StartMatchMaker();
        }
        networkManager.matchMaker.ListMatches(0,100,"",true,0,0, OnMatchList);
    }

    public void SetRoomName(string name){
        roomName = name;
    }

    public void CreateRoom(){
        if(roomName != "" && roomName != null){
            Debug.Log("Creating room: " + roomName + "with room for " + roomSize + " player");

            //create room 
            networkManager.matchMaker.CreateMatch(roomName,roomSize,true,"","","",0,0,networkManager.OnMatchCreate);
        }
    }

    //Die funktion bekommt eine liste von room. Wir suchen unseren Raum in der Liste. 
    //Wenn es denn noch nicht gibt erstellen wir selbst einen mit dem namen den wir von QR gelesen haben; 
    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
        string message = "Success: " + success + "; Ext Info: " + extendedInfo + "; MatchList: ";
        foreach(var match in matchList){
            //if found BattleGround join game like this
            //networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
            networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
            message += match.name + "; ";
        }
       
        Debug.Log(message);
    }

    //Die funktion bekommt eine liste von room. Wir suchen unseren Raum in der Liste. 
    //Wenn es denn noch nicht gibt erstellen wir selbst einen mit dem namen den wir von QR gelesen haben; 
    public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo){  
        Debug.Log("JOINED "+ success + " . " + extendedInfo + "; Info: " + matchInfo.address + ", " + matchInfo.ToString());
    }
	
}
