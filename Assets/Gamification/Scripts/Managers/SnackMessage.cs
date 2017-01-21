using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnackMessage : MonoBehaviour {
    protected SnackMessage () {} // guarantee this will be always a singleton only - can't use the constructor!
    public Text messageLabel; 
    public GameObject messageObject;
    private int duration = 2;
	// Use this for initialization
	void Start () {
		
	}
    public SnackMessage For(int sec){
        duration = sec;
        return this;
    }
    private void Hide(){
        messageObject.SetActive(false);
        duration = 2;
    }

    private void _Show(object message){  
        messageLabel.text = message.ToString();
        messageObject.SetActive(true);
        Invoke("Hide", duration);
    }

    public void Show(string message){
        _Show(message);
    }
    public void ShowOnMainThread(string message){
        MainThread.Call(_Show, message);
    }
}
