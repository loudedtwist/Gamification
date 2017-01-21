using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parse;

public class LogOut : MonoBehaviour {

    public void LogOutUser(){
        if(ParseUser.CurrentUser != null) ParseUser.LogOutAsync();
        GuiManager.Instance.ShowLoginPage();
    }
}
