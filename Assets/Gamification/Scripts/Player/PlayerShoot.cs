using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour { 
    public Text log;
    int uniqueInt;
    void Start(){
        uniqueInt = Random.Range(1,1999); 
    }

    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer) return; 
        if(Input.GetKey("f")){
            //auf der Klientseite "f" drücken, shoot soll auf server ausgeführt werden
            Shoot();
        }
        if(Input.GetKeyUp("e")){ 
            //1. Click "e" auf dem server -> auf der Klientseite wird in TakeDamage ausgeführt
            TakeDamage(-1);
        }
    }

    /// <summary>
    /// FROM CLIENT TO SERVER
    /// </summary>
    [Client]
    void Shoot(){
        if (!isClient) return;
        CmdPlayerShot(transform.name,uniqueInt);
    }

    [Command] //only on the server, also from client to server
    void CmdPlayerShot(string _ID, int randomInt){
        //transform.gameObject.SetActive(false);
        Debug.Log(_ID + " has been schot.(server?). This Id came from client -> its client ID" + transform.name + " IS MY ID");
        Debug.Log("IS SERVER: " + isServer + "; Random from Client: " + randomInt + "; Random Server: " + uniqueInt);
        RpcDamage(randomInt);
    }




    /// <summary>
    /// FROM SERVER TO CLIENT
    /// </summary>
    [SyncVar]
    int health = 1000000000;

    int local;

    [ClientRpc]
    void RpcDamage(int amount)
    {
        if (transform.name == "Player 1")
        {
            GuiManager.Instance.WriteToLog("CLient got id from Server: " + amount + " PLAYER 1");
        }
        else
        {
            GuiManager.Instance.WriteToLog("CLient got id from Server: " + amount);
            Debug.LogError("Took damage:" + amount + "; MY ID:" + transform.name + " IS CLIENT " + isClient + " random ID " + uniqueInt + " From server" + amount);
        }
    }

    public void TakeDamage(int amount)
    {
        if (!isServer) return;

        health -= amount;
        RpcDamage(amount);
    }
}
