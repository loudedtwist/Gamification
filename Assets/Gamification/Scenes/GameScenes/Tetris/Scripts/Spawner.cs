using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public GameObject[] groups;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void spawnNext() {
		// Random Index
		int i = Random.Range(0, groups.Length);

		// Spawn Group at current Position
		var spawnedObj = Instantiate(groups[i],
			transform.position,
			Quaternion.identity);
        spawnedObj.transform.parent = gameObject.transform;
	}
}
