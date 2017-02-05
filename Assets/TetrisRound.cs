using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisRound : MonoBehaviour {

	public static int points;
	public GameObject[] groups;
	public static string scoreString;

	// Use this for initialization
	void Start () {
		
	}

	void OnEnable(){
		
		points = 0;
		groups = GameObject.FindGameObjectsWithTag("TetrisGroup");
		foreach (GameObject x in groups){
			Destroy (x);
		}
		Grid.resetGrid ();
		FindObjectOfType<Spawner>().spawnNext();
		GameObject.Find ("debugText").GetComponent<Text> ().text = "";
		GameObject.Find ("Score").GetComponent<Text> ().text = "Score: 00000";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void addPoints(int p){
		points += p;
		scoreString = string.Format ("{0} {1}", "Score:", points.ToString("D5"));
		GameObject.Find ("Score").GetComponent<Text> ().text = scoreString;
	}
}
