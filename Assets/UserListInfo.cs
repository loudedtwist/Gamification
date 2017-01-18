using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserListInfo : MonoBehaviour {
    public Text usersList;
	// Use this for initialization
	void Start () {
		
	}

    void Update(){
        var players = GameObject.FindGameObjectsWithTag("Player");
        if (players == null)
        {
            Debug.LogError("NO USERS TAG FOUND");
            return;
        }
        string users = "";
        foreach(var user in players){
            users += user.gameObject.transform.name + "\n";
        }
        usersList.text  = users;
    }
}
