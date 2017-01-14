using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GuiManager : SimpleSingleton<GuiManager> { 
    protected GuiManager () {} // guarantee this will be always a singleton only - can't use the constructor!

    public GameObject[] pages;
    public LoadingManager loading;
    public SnackMessage message;

    void Start(){
        ShowPage("LoginPage");
    }


    public void ShowSignUpAditionalInfoPage(){ 
        MainThread.Call(ShowPage,"SignUpAdditionalInfo");
    }

    public void ShowLoginPage(){  
        MainThread.Call(ShowPage, "LoginPage");
    }  

    public void ShowUserHomePage(){  
        MainThread.Call(ShowPage, "UserHomePage");
    }   

    public void ShowBattleGroundLobbyPage(){ 
        MainThread.Call(ShowPage, "BattleGroundLobbyPage");
    }
    public void ShowWaitingForUserPage(){
        MainThread.Call(ShowPage, "WaitingForUsersPage");
    }

    private void ShowPage(object pageNameObj){
        var pageName = pageNameObj.ToString();
        foreach(var page in pages){
            if (page.name == pageName)
                page.SetActive(true);
            else
                page.SetActive(false);
        }  
        Debug.Log("GUI : SWITCHED TO " + pageName +" PAGE");
    } 


    public Text log;
    public Text connectedUsers;
    public void SetConnectedUsersLabel(int num){ 
        connectedUsers.text = ""+num;
    }
    public void WriteToLog(string text){
        if (log == null)
            log = GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>();
        log.text = text;
    }
} 

