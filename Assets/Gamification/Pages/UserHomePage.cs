using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse; 

public class UserHomePage : MonoBehaviour {


    private ParseUser user = null; 
    public Text userName;
    public Text sNummer;
    public Text faculty; 

    void OnEnable(){
        user = ParseUser.CurrentUser;
        if (user == null)
        {
            GuiManager.Instance.ShowLoginPage();
            return;
        } 
        if (userName == null || sNummer == null || faculty == null)
            return; 
        userName.text = user.Username;
        sNummer.text = user.Get<string>("sNummer");
        faculty.text = user.Get<string>("faculty");
    }

}
