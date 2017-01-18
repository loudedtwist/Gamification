using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parse;

public class PlayerName : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.name = ParseUser.CurrentUser.Username;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
