using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Parse;

public class PlayerInfo : MonoBehaviour {

    public string name;
    public Text nameLabel;
	// Use this for initialization
	void Start () {
        name = ParseUser.CurrentUser.Username;
        nameLabel.text = name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
