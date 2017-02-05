using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GuiManager : SimpleSingleton<GuiManager>
{
    protected GuiManager()
    {
    }
    // guarantee this will be always a singleton only - can't use the constructor!

    public bool showSplashScreen;
    public List<GameObject> pages;
    public LoadingManager loading;
    public SnackMessage message;
    public GameObject arCam;
    public GameObject guiLayer;
	public Camera camMain;
	public Camera camTetris;

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

    public void ShowQuizResultsPage()
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

    public void HideAllPages()
    {
        ShowPage("hgakjkshdkjsahdja");
    }

    public void ShowLoginPage()
    {
        MainThread.Call(ShowPage, "LoginPage");
    }

    public void ShowUserHomePage()
    {
        MainThread.Call(ShowPage, "UserHomePage");
    }

    public void ShowUserStatsPage()
    {
        MainThread.Call(ShowPage, "UserStatsPage");
    }

    public void ShowBattleGroundLobbyPage()
    {
        MainThread.Call(ShowPage, "BattleGroundLobbyPage");
    }
    public void ShowBattleGroundFromQRPage(string roomName)
    {
        ShowPage("BattleGroundQRLobbyPage");
        var lobbyPage = FindPage("BattleGroundQRLobbyPage");
        if( lobbyPage == null){
            Debug.LogError("Lobby page not found");
        }
        var hostScript = lobbyPage.GetComponentInChildren<HostGameFromQR>();
        if( hostScript == null){
            Debug.LogError("Host script not found");
        }
        hostScript.CreateOrConnectToRoom(roomName);
    }
    public void ShowBattleGroundNamedLobbyPage()
    {
        MainThread.Call(ShowPage, "BattleGroundNamedLobbyPage");
    }

    public void ShowWaitingForUserPage()
    {
        MainThread.Call(ShowPage, "WaitingForUsersPage");
    }

    public void ShowSplashPage()
    {
        MainThread.Call(ShowPage, "SplashPage");
    }

	//TODO - Tetris as page?
	public void ShowTetrisPage()
	{
		guiLayer.gameObject.SetActive(false);
        var tetrisPage = FindPage("Tetris");
        if(tetrisPage == null){
            Debug.LogError("TETRIS PAGE NOT FOUND ");
			//pages.Add ();
            return;
        }
        tetrisPage.SetActive(true);
		camMain.gameObject.SetActive(false);
		camTetris.gameObject.SetActive(true);
	}

	public void BackFromTetrisPage()
	{
		Debug.LogWarning("Tetris Ended");
		camTetris.gameObject.SetActive(false); 
        FindPage("Tetris").SetActive(false);
		camMain.gameObject.SetActive(true);
		guiLayer.gameObject.SetActive(true);
	}

    private void ShowPage(object pageNameObj)
    {
        var pageName = pageNameObj.ToString();
        GameObject pageToShow = null ;

        foreach (var page in pages)
        {
            if (page.name == pageName) pageToShow = page; 
            page.SetActive(false);
        } 

        if (pageToShow != null)
        {
            pageToShow.SetActive(true);
        }
        Debug.Log("GUI : SWITCHED TO " + pageName + " PAGE");
    }

    private GameObject FindPage(string name){
        foreach (var page in pages)
        {
            if (page.name == name) return page;
        } 
        return null;
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