
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse;

public class LoginPage : MonoBehaviour
{

    public InputField nameInput;
    public InputField passwInput;

    private ParseUser user = null;

    void Start()
    {  
        user = ParseUser.CurrentUser;
        if (user == null)
        {
            GuiManager.Instance.ShowLoginPage();
        }
        else
        { 
            GuiManager.Instance.ShowUserHomePage();
            //user is signed up and cant work -> show his home page
        }
    }

    public void CreateUser()
    {
        GuiManager.Instance.loading.Show();

        var user = new ParseUser()
        {
            Username = nameInput.text,
            Password = passwInput.text
        }; 

        try
        {
            user.SignUpAsync().ContinueWith(t =>
                {
                    if (t.IsFaulted || t.IsCanceled)
                    {
                        GuiManager.Instance.loading.HideOnMainThread();
                        // Errors from Parse Cloud and network interactions
                        using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
                        {
                            if (enumerator.MoveNext())
                            {
                                ParseException error = (ParseException)enumerator.Current; 
                                GuiManager.Instance.message.For(2).ShowOnMainThread(error.Message);
                                Debug.LogError("ERROR: " + error.Message); 
                                Debug.LogError("Name: " + nameInput.text + " Passw: " + passwInput.text);
                            }
                        }
                    }
                    else
                    { 
                        GuiManager.Instance.loading.HideOnMainThread();
                        Debug.Log("SIGNUP: " + "SUCCESS"); 
                        GuiManager.Instance.ShowSignUpAditionalInfoPage();
                    }

                });
        }
        catch (InvalidOperationException e)
        {
            Debug.LogError("ERROR: " + e.Message);
        }
    }

    public void LogIn()
    {

        GuiManager.Instance.loading.Show();

        ParseUser.LogInAsync(nameInput.text, passwInput.text).ContinueWith(t =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    Debug.LogError("ERROR: " + "Login failed");
                    foreach (var e in t.Exception.InnerExceptions)
                    {
                        Debug.LogError("LOGIN ERROR: " + e.Message); 
                        GuiManager.Instance.message.For(2).ShowOnMainThread(e.Message);   
                    }
                }
                else
                {
                    user = t.Result;
                    Debug.LogError("DONE: " + "Login was successful");
                    GuiManager.Instance.ShowUserHomePage();
                } 
                GuiManager.Instance.loading.HideOnMainThread();
            });
    }
}
