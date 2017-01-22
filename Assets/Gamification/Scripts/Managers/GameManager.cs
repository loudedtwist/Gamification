using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text readyGoBanner;
    public string readyText;
    public int secondsUntilStart = 3;

    // Use this for initialization
    void Start()
    {
        readyText = "Game starts in ";
    }


    public void StartGame()
    {
        readyGoBanner.gameObject.transform.parent.gameObject.SetActive(true);
        Invoke("ChangeToQuizPage", 4.0f);
        InvokeRepeating("DecrementTimeToStart", 0.0f, 1.0f);
    }

    public void DecrementTimeToStart()
    {
        if (secondsUntilStart == 0)
        {
            CancelInvoke("DecrementTimeToStart");
            readyGoBanner.text = "GO";
            return;
        }
        readyGoBanner.text = readyText + secondsUntilStart;
        --secondsUntilStart;
    }

    public void ChangeToQuizPage()
    {
        readyGoBanner.gameObject.transform.parent.gameObject.SetActive(false);
        GuiManager.Instance.ShowQuizPage();
    }

    public void FinishGame()
    {
    }
}