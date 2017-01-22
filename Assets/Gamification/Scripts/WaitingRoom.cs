using System.Collections;
using System.Collections.Generic;
using Parse;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class WaitingRoom : NetworkBehaviour
{
    public const string Joined = "Player.Joined";

    [SyncVar] public string players = "";

    public Text playersList;

    void Start()
    {
        InvokeRepeating("showConnectedUserNumIfServer", 1, 1);
    } 

    public void showConnectedUserNumIfServer(){ 
        if (isServer && isLocalPlayer)
        {
            showConnectedUserNum();
        }
    }

    [Command]
    public void CmdUserJoined(string name)
    {
        players += name + "\n";
        Debug.LogError("MESSAGE FROM SERVER ; USER JOINED IS SERVER: " + isServer);
        RpcUpdateGuiPlayerList(name);
    }

    [ClientRpc]
    public void RpcUpdateGuiPlayerList(string players)
    {
        //playersList.text = players;
        GuiManager.Instance.SetUserList(players);
    }

    [ClientRpc]
    public void RpcUpdateUserCount(int players)
    {
        GuiManager.Instance.SetConnectedUsersLabel(players);
    }

    //[Command]
    public void showConnectedUserNum()
    {
        int playersCount = NetworkLobbyManager.singleton.numPlayers;
        RpcUpdateUserCount(playersCount);
    }

    public override void OnStartLocalPlayer()
    {
        if (ParseUser.CurrentUser.Username == "pixar")
            CmdUserJoined(ParseUser.CurrentUser.Username + " User Added");
    }
}