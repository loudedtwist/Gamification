using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GuiManager : Singleton<GuiManager> { 
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
    public void WriteToLog(string text){
        log.text = text;
    }
} 

