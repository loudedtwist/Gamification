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

    private float secondsUntilStartCounter;

    public void StartGame()
    {
        ShowBanner(true);
        secondsUntilStartCounter = secondsUntilStartVal;
        Invoke("ChangeToQuizPage", secondsUntilStartVal + 1.0f);
        Invoke("FinishGame", secondsUntilStartVal + 1.0f + gameDuration);
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
        Debug.LogAssertion("SCORE PAGE ");
        Debug.LogAssertion("SCORE PAGE #");
        Debug.LogAssertion("SCORE PAGE ##");
        Debug.LogAssertion("SCORE PAGE ###");
        Debug.LogAssertion("SCORE PAGE ####");
        //go to score page
    }

    void ShowBanner(bool state)
    {
        readyGoBanner.gameObject.transform.parent.gameObject.SetActive(state);
    }
}