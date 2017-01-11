
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse;

public class LoginPage : MonoBehaviour {

	public InputField nameInput;
	public InputField passwInput;
    public GuiManager guiManager;

	private ParseUser user = null;

	void Start()
	{  
        user = ParseUser.CurrentUser;
		if (user == null)
		{
			//show login page
		}
		else
		{ 
			//user is signed up and cant work -> show his home page
		}
	}

	// Update is called once per frame
	void Update()
	{

	}


	public void CreateUser()
	{
		var user = new ParseUser()
		{
			Username = nameInput.text,
			Password = passwInput.text/*,
			Email = "email@example.com"*/
		};
		//user["phone"] = objName.text;

		try
		{
			user.SignUpAsync().ContinueWith(t =>
			{
				if (t.IsFaulted || t.IsCanceled)
				{
					// Errors from Parse Cloud and network interactions
					using (IEnumerator<System.Exception> enumerator = t.Exception.InnerExceptions.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							ParseException error = (ParseException)enumerator.Current;
                            Debug.LogError("ERROR: " + error.Message);
                            Debug.LogError("Name: " + nameInput.text + " Passw: " + passwInput.text);
						}
					}
				}
				else
				{
                    Debug.Log("SIGNUP: " + "SUCCESS");
                    guiManager.ShowSignUpAditionalInfoPage();
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

	public void LogOut()
	{
		ParseUser.LogOut();
	}
}
