﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GuiManager : SimpleSingleton<GuiManager>
{
    protected GuiManager()
    {
    } // guarantee this will be always a singleton only - can't use the constructor!

    public bool showSplashScreen;
    public GameObject[] pages;
    public LoadingManager loading;
    public SnackMessage message;
    public GameObject arCam;
    public GameObject guiLayer;

    void Start()
    {
        if (showSplashScreen)
        {
            ShowPage("SplashPage");

            Invoke("ShowLoginPage", 2.0f);
        }
        else
        {
            ShowPage("LoginPage");
        }
    }

    public void ShowQuizResults()
    {
        MainThread.Call(ShowPage, "QuizResults");
    }

    public void ShowSignUpAditionalInfoPage()
    {
        MainThread.Call(ShowPage, "SignUpAdditionalInfo");
    }

    public void ShowQuizPage()
    {
        MainThread.Call(ShowPage, "QuizPage");
    }

    public void ShowLoginPage()
    {
        MainThread.Call(ShowPage, "LoginPage");
    }

    public void ShowUserHomePage()
    {
        MainThread.Call(ShowPage, "UserHomePage");
    }

    public void ShowBattleGroundLobbyPage()
    {
        MainThread.Call(ShowPage, "BattleGroundLobbyPage");
    }

    public void ShowWaitingForUserPage()
    {
        MainThread.Call(ShowPage, "WaitingForUsersPage");
    }

    public void ShowSplashPage()
    {
        MainThread.Call(ShowPage, "SplashPage");
    }

    private void ShowPage(object pageNameObj)
    {
        var pageName = pageNameObj.ToString();
        foreach (var page in pages)
        {
            if (page.name == pageName)
                page.SetActive(true);
            else
                page.SetActive(false);
        }
        Debug.Log("GUI : SWITCHED TO " + pageName + " PAGE");
    }

    // QR SCANNER
    public void ShowQrScannerPage()
    {
        arCam.gameObject.SetActive(true);
        guiLayer.gameObject.SetActive(false);
    }

    public void BackFromQrScannerPage()
    {
        Debug.LogWarning("BACK PRESSED");
        arCam.gameObject.SetActive(false);
        guiLayer.gameObject.SetActive(true);
    }


    public Text log;
    public Text connectedUsers;
    public Text connectedUserList;

    public void SetConnectedUsersLabel(int num)
    {
        connectedUsers.text = "" + num;
    }

    public void WriteToLog(string text)
    {
        if (log == null)
            log = GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>();
        log.text = text;
    }

    public void SetUserList(string users)
    {
        Debug.LogError("GUI MANAGER SET USER" + users);
        connectedUserList.text = users;
    }
}