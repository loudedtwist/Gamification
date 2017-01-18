using System.Collections;
using System.Collections.Generic;
using Parse;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class WaitingRoom : NetworkBehaviour {
    
    public const string Joined = "Player.Joined";

    [SyncVar]
    public string players = "";

    public Text playersList;
    //[SyncVar]
    //public int playersCount = 0; 

	// Use this for initialization
	void Start () { 
        //InvokeRepeating("CmdShowConnectedUserNum", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if(isServer && isLocalPlayer){
            showConnectedUserNum();
        }
    }

    [Command]
    public void CmdUserJoined(string name){
        players += name +"\n";
        Debug.LogError("MESSAGE FROM SERVER ; USER JOINED IS SERVER: "  + isServer);
        RpcUpdateGuiPlayerList(name); 

    }

    [ClientRpc]
    public void RpcUpdateGuiPlayerList(string players){ 
        //playersList.text = players;
        GuiManager.Instance.SetUserList(players); 
    }

    [ClientRpc]
    public void RpcUpdateUserCount(int players){
        GuiManager.Instance.SetConnectedUsersLabel(players);

    }

    //[Command]
    public void showConnectedUserNum(){  
        var isServer = Network.isServer;
        int playersCount = NetworkLobbyManager.singleton.numPlayers; 

        string log = "IS SERVER: " + isServer;
        log += "\n";
        log += "COUNT USERS: " + playersCount;
        log += "\n";  
        Debug.LogError(log);
        RpcUpdateUserCount(playersCount); 
    }

    public override void OnStartLocalPlayer(){ 
        if(ParseUser.CurrentUser.Username == "pixar")
            CmdUserJoined(ParseUser.CurrentUser.Username + " User Added"); 
    }
}
