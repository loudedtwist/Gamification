using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WaitingForUsersPage : MonoBehaviour {
    public UsersConnection connection; 
    private uint roomSize = 6; 
    private NetworkManager networkManager;
    int playerCount = 0;

    void Start(){
        if(connection==null) Debug.LogError("UsersConnection script not set in WaitingForUsersPage");
        networkManager = NetworkManager.singleton; 
    } 
    void OnEnable(){ 
        InvokeRepeating("ShowConnectedUserNum", 1, 1);
    }
    void OnDisable(){
        CancelInvoke("ShowConnectedUserNum");
    }
    void OnDestroy(){
        CancelInvoke();
    } 
    public void ShowConnectedUserNum(){  
        var isServer = Network.isServer;
        var newPlayerCount = NetworkLobbyManager.singleton.numPlayers;  
        if(playerCount != newPlayerCount){ 
            playerCount = newPlayerCount;
        }

        string log = "IS SERVER: " + isServer; log += "\n";
        log += "COUNT USERS: " + newPlayerCount; log += "\n";  
        Debug.LogError(log);
        GuiManager.Instance.SetConnectedUsersLabel(newPlayerCount);
    }
}