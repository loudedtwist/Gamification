using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {
    public GameObject loadingCircle;
    public Text loadingLabel;



    public void HideOnMainThread(){
        MainThread.Call(Hide);
    }
    public void Hide(){
        loadingCircle.SetActive(false);
    }

    public void ShowOnMainThread(){
        MainThread.Call(Show);
    }
    public void Show(){
        loadingCircle.SetActive(true);
    }

    public void SetTextOnMainThread(string text){
        MainThread.Call(SetText,text);
    }
    public void SetText(object text){
        loadingLabel.text = text.ToString();
    }
}
