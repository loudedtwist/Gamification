using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QrPage : MonoBehaviour {
    public GameManager gameManager; 
    public HostGameFromQR host; 
	void OnEnable () {
        Debug.LogError("QR PAGE");
        gameManager.StopGame(); 
        //host.Restart();
	}
	 
}
