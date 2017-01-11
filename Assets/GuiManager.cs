using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


public class GuiManager : MonoBehaviour { 
    public GameObject[] pages;

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

} 

