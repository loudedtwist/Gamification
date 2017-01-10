
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse;

public class TestParse : MonoBehaviour
{
    public InputField nameInput;
    public InputField passwInput;
    public InputField objName;

    private ParseUser user = null;

    void Start()
    { 
        if(ParseUser.CurrentUser==null){
            //show login page
        } else {
            user = ParseUser.CurrentUser;
            //user is signed up and cant work -> show his home page
        }
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void CreateTestObject()
    { 

        Debug.Log("TestObjectCreated");
        ParseObject testObject = new ParseObject("TestObject");
        testObject["foo"] = "bar";
        try
        {
            testObject.SaveAsync().ContinueWith(task =>
                { 
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        Debug.LogError("Error, cant create data");
                        // Errors from Parse Cloud and network interactions
                        using (IEnumerator<System.Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
                        {
                            if (enumerator.MoveNext())
                            {
                                ParseException error = (ParseException)enumerator.Current;

                                Debug.LogError("ERROR: " + error.Message);
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("Has been created"); 
                    }
                });
        }
        catch (InvalidOperationException e)
        {
            Debug.LogError("ERROR: " + e.Message);
        }
    }
    public void CreateUser(){
        var user = new ParseUser(){
            Username = nameInput.text,
            Password = passwInput.text,
            Email = "email@example.com"
        }; 
        user["phone"] = objName.text;

        try
        {
            user.SignUpAsync().ContinueWith(t => {
                if (t.IsFaulted || t.IsCanceled) {
                    // Errors from Parse Cloud and network interactions
                    using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator()) {
                        if (enumerator.MoveNext()) {
                            ParseException error = (ParseException) enumerator.Current;
                            Debug.LogError("ERROR: " + error.Message); 
                        }
                    }
                }
                else{ 
                    Debug.Log("LOGIN: " + "SUCCESS"); 
                }
            });
        }
        catch (InvalidOperationException e)
        {
            Debug.LogError("ERROR: " + e.Message);
        }
    }
    public void CreateCustomObj(){ 
        ParseObject bfo = new ParseObject(objName.text);
        bfo["foo"] = "bar"; 
        bfo["score"] = "1000"; 
        bfo["var"] = 100;
        bfo.SaveAsync();
    }

    public void LogIn(){
        ParseUser.LogInAsync(nameInput.text, passwInput.text).ContinueWith(t =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    Debug.LogError("ERROR: " + "Login failed");
                    foreach (var e in t.Exception.InnerExceptions)
                    {
                        Debug.LogError("LOGIN ERROR: " + e.Message); 
                    }
                }
                else
                {
                    user = t.Result;
                    Debug.LogError("DONE: " + "Login was successful"); 
                }
            });
    }

    public void LogOut(){
        ParseUser.LogOut();
    }
}
