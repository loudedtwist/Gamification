using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Question : NetworkBehaviour
{
    [SyncVar(hook = "OnChangeQ")]
    public string question1 = "";

    public void OnChangeQ(string newQ)
    {
        Debug.LogError("CHANGED TEXT QUESTION!!!");
        question1 = newQ;
    }

    public void Start()
    {
        Invoke("ChangeMyQuestion", 3.0f);
    }

    public void ChangeMyQuestion()
    {
        if(!isServer) return;
        question1 = "gaaaaay??";
    }
}
