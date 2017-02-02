using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Group : MonoBehaviour {


	// Time since last gravity tick
	float lastFall = 0;
	Vector2 swipeDelta = new Vector2();
	float timeDelta = 0;

	// Use this for initialization
	void Start () {
		// Default position not valid? Then it's game over
		if (!isValidGridPos()) {
			GameObject.Find ("debugText").GetComponent<Text> ().text = "GAME OVER";
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		int touchSum = 0;
		Touch[] myTouches = Input.touches;
		for (int i = 0; i < Input.touchCount; i++) {
			Vector2 v = myTouches[i].position;
			if (myTouches [i].phase == TouchPhase.Began) {
				swipeDelta = v;
				timeDelta = Time.time;
			}
			else if (myTouches [i].phase == TouchPhase.Ended) {
				swipeDelta -= v;
				timeDelta -= Time.time;
				if (timeDelta > -0.5) {
					if (swipeDelta.x > 100)
						moveLeft ();
					else if (swipeDelta.x < -100)
						moveRight ();
					else
						rotateLeft ();
				}
			}
			/*
			else if (v.x < (Screen.width / 2) && myTouches[i].phase == TouchPhase.Ended)
				moveLeft();
			else if (v.x > (Screen.width / 2) && myTouches[i].phase == TouchPhase.Ended)
				moveRight();
			*/

		}
			
		// Move Left
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			moveLeft ();
		}

		// Move Right
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			moveRight ();
		}

		// Rotate
		else if (Input.GetKeyDown(KeyCode.UpArrow)) {
			rotateLeft();
		}

		// Move Downwards and Fall
		else if (Input.GetKeyDown(KeyCode.DownArrow) ||
			Time.time - lastFall >= 1) {
			// Modify position
			transform.position += new Vector3(0, -1, 0);

			// See if valid
			if (isValidGridPos()) {
				// It's valid. Update grid.
				updateGrid();
			} else {
				// It's not valid. revert.
				transform.position += new Vector3(0, 1, 0);

				// Clear filled horizontal lines
				Grid.deleteFullRows();

				// Spawn next Group
				FindObjectOfType<Spawner>().spawnNext();

				// Disable script
				enabled = false;
			}

			lastFall = Time.time;
		}
		
	}

	void rotateLeft(){
		transform.Rotate(0, 0, -90);

		// See if valid
		if (isValidGridPos())
			// It's valid. Update grid.
			updateGrid();
		else
			// It's not valid. revert.
			transform.Rotate(0, 0, 90);
	}
	void moveLeft(){
		// Modify position
		transform.position += new Vector3 (-1, 0, 0);

		// See if valid
		if (isValidGridPos ())
			// Its valid. Update grid.
			updateGrid ();
		else
			// Its not valid. revert.
			transform.position += new Vector3 (1, 0, 0);
	}

	void moveRight(){
		// Modify position
		transform.position += new Vector3 (1, 0, 0);

		// See if valid
		if (isValidGridPos ())
			// It's valid. Update grid.
			updateGrid ();
		else
			// It's not valid. revert.
			transform.position += new Vector3 (-1, 0, 0);
	}

	bool isValidGridPos() {        
		foreach (Transform child in transform) {
			Vector2 v = Grid.roundVec2(child.position);

			// Not inside Border?
			if (!Grid.insideBorder(v))
				return false;

			// Block in grid cell (and not part of same group)?
			if (Grid.grid[(int)v.x, (int)v.y] != null &&
				Grid.grid[(int)v.x, (int)v.y].parent != transform)
				return false;
		}
		return true;
	}

	void updateGrid() {
		// Remove old children from grid
		for (int y = 0; y < Grid.h; ++y)
			for (int x = 0; x < Grid.w; ++x)
				if (Grid.grid[x, y] != null)
				if (Grid.grid[x, y].parent == transform)
					Grid.grid[x, y] = null;

		// Add new children to grid
		foreach (Transform child in transform) {
			Vector2 v = Grid.roundVec2(child.position);
			Grid.grid[(int)v.x, (int)v.y] = child;
		}        
	}
}
