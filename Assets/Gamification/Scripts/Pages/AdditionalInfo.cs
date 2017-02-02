using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse;

public class AdditionalInfo : MonoBehaviour {
	public InputField sNummer;
	public Dropdown faculty; 
    public ParseUser user; 
    public GuiManager guiManager;

	public void Done()
    {
        user = ParseUser.CurrentUser;
        if(user==null){
            Debug.LogError("USER NOT FOUND !!!!!");
            //show log in page
        }

        user["sNummer"] = sNummer.text;
        user["faculty"] = faculty.captionText.text;
        user["score"] = 0;

        user["mirrors"] = 50;
        user["noiz"] = 2;
        user["booms"] = 2;

        user.SaveAsync().ContinueWith(task => {
            if(task.IsCompleted && (task.IsFaulted || task.IsCanceled)){
                Debug.LogError("Cant save additional user data");
            }
            else{
                Debug.Log("Additional user data saved"); 
                guiManager.ShowUserHomePage();
            }
        });
	}
}
