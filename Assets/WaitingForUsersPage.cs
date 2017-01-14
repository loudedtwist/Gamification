using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WaitingForUsersPage : MonoBehaviour {
    
    private uint roomSize = 6; 
    private NetworkManager networkManager;

    void Start(){
        networkManager = NetworkManager.singleton;
        if(networkManager.matchMaker == null ){
            networkManager.StartMatchMaker();
        }
        InvokeRepeating("ShowConnectedUserNum", 1, 1);
    } 
    void OnDestroy(){
        CancelInvoke();
    }
	// Update is called once per frame
	void Update () {
	}

    public void ShowConnectedUserNum(){ 
        var isServer = Network.isServer;
        var numPlayer = NetworkLobbyManager.singleton.numPlayers; 
        string log = "IS SERVER: " + isServer; log += "\n";
        log += "COUNT USERS: " + numPlayer; log += "\n";  
        Debug.LogError(log);
        GuiManager.Instance.SetConnectedUsersLabel(numPlayer);
    }
}
