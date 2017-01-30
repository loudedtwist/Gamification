using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text readyGoBanner;
    public string readyText = "Game starts in ";
    public float secondsUntilStartVal = 3.0f;

    public float gameDuration = 20.0f;

    //change rounds in NetworkObject index generator
    public int roundsNumber = 6;

    [SerializeField] private int roundsRemains;
    public QuestionLoader questionLoader;

    private float secondsUntilStartCounter;

    void Start()
    {
        roundsRemains = roundsNumber;
    }

    public void StartGame()
    {
        secondsUntilStartCounter = secondsUntilStartVal;
        ShowBanner(true);

        Invoke("ChangeToQuizPage", secondsUntilStartVal + 1.0f);
        InvokeRepeating("StartRound", secondsUntilStartVal + 1.0f, gameDuration);
        Invoke("FinishGame", secondsUntilStartVal + 1.0f + gameDuration * roundsNumber);
        InvokeRepeating("DecrementTimeToStart", 0.0f, 1.0f);
    }

    public void DecrementTimeToStart()
    {
        if (secondsUntilStartCounter < 1)
        {
            CancelInvoke("DecrementTimeToStart");
            readyGoBanner.text = "GO";
            return;
        }
        readyGoBanner.text = readyText + secondsUntilStartCounter;
        --secondsUntilStartCounter;
    }

    public void ChangeToQuizPage()
    {
        ShowBanner(false);
        GuiManager.Instance.ShowQuizPage();
    }

    public void FinishGame()
    {
        GuiManager.Instance.ShowQuizResultsPage();

        //go to score page
    }

    public void StartRound()
    {
        --roundsRemains;

        Debug.LogAssertion("##############");
        Debug.LogAssertion("ROUND: " + (roundsNumber - roundsRemains + 1) + " #");
        Debug.LogAssertion("ROUNDS REMAINS: " + roundsRemains + " #");
        Debug.LogAssertion("############## ");
        GuiManager.Instance.ShowQuizPage();

        if (roundsRemains < 1)
        {
            Debug.LogAssertion("STOPPING CORUTION");
            CancelInvoke("StartRound");
        }
    }

    void ShowBanner(bool state)
    {
        readyGoBanner.gameObject.transform.parent.gameObject.SetActive(state);
    }
}