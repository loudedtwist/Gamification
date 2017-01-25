﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

using UnityEngine.Networking.Match;

public class HostGame : MonoBehaviour {

    public PlayerGui playerGui; 

    [SerializeField]
    private uint roomSize = 6; 
    private string roomName;
    private NetworkManager networkManager;

    private NetworkID netId;
    private NodeID myNodeId;

    void OnEnable(){
        networkManager = NetworkManager.singleton;

        if(networkManager.matchMaker != null){ 
            DropPreviousMatch();
        }
        if(networkManager.matchMaker == null ){ 
            networkManager.StartMatchMaker();
        } 
        networkManager.matchMaker.ListMatches(0,100,"",true,0,0, OnMatchList);
    }

    public void SetRoomName(string name){
        roomName = name;
    }

    void DropPreviousMatch()
    {   
        networkManager.matchMaker.DropConnection(netId, myNodeId, 0, OnDropConnection);
    }

    public void CreateRoom(){
        if(roomName != "" && roomName != null){
            Debug.Log("Creating room: " + roomName + "with room for " + roomSize + " player"); 
            //create room 
            CheckNetworkManager();
            networkManager.matchMaker.CreateMatch(roomName,roomSize,true,"","","",0,0, OnMatchCreate);
        }
    }

    //Die funktion bekommt eine liste von room. Wir suchen unseren Raum in der Liste. 
    //Wenn es denn noch nicht gibt erstellen wir selbst einen mit dem namen den wir von QR gelesen haben; 
    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList){
        string message = "Success: " + success + "; Ext Info: " + extendedInfo + "; MatchList: ";
        foreach(var match in matchList){
            //if found BattleGround join game like this
            //networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
            CheckNetworkManager();
            networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);

            netId = match.networkId;

            message += match.name + "; ";
        }
       
        Debug.Log(message);
    }

    void CheckNetworkManager()
    {
        if (networkManager == null)
        {
            Debug.LogError("NETWORK MANAGER IS NULL");
            return;
        }
        if (networkManager.matchMaker == null)
        {
            Debug.LogError("NETWORK matchMaker IS NULL");
            networkManager.StartMatchMaker();
            return;
        }
    }

    public void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo){
        Debug.Log("CREATED room: " + success + "; Info:" + matchInfo.ToString() ); 
        if (success)
        { 
            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, 7777); 
            networkManager.StartHost(hostInfo); 

            netId = matchInfo.networkId;
            myNodeId = matchInfo.nodeId;

            GuiManager.Instance.ShowWaitingForUserPage(); 
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    //Die funktion bekommt eine liste von room. Wir suchen unseren Raum in der Liste. 
    //Wenn es denn noch nicht gibt erstellen wir selbst einen mit dem namen den wir von QR gelesen haben; 
    public void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo){  
        Debug.Log("JOINED "+ success + " . " + extendedInfo + "; Info: " + matchInfo.address + ", " + matchInfo.ToString());
        if (success)
        { 
            MatchInfo hostInfo = matchInfo;
            networkManager.StartClient(hostInfo);

            myNodeId = matchInfo.nodeId;
 
            GuiManager.Instance.ShowWaitingForUserPage();
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }
	
    void OnDropConnection(bool success, string extendedInfo)
    {
        string message = "prev droped: " + success; 
        GuiManager.Instance.message.For(2).Show(message); 
        NetworkManager.singleton.StopMatchMaker(); 
    }
}
