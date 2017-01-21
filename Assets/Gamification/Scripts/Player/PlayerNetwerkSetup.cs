using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerNetwerkSetup : NetworkBehaviour {
    
    private const string PLAYER_TAG = "Player";

    [SerializeField]
    Behaviour[] componentsToDisable;
    private Camera sceneCamera;


	// Use this for initialization
	void Start () {
        if(!isLocalPlayer){ 
            DisableComponents();
        }else{
            //TurnOfMainCamera();
        }
        GetUniquePlayerName();
	}

    void DisableComponents()
    {
        foreach (var comp in componentsToDisable)
        {
            comp.enabled = false;
        }
    }

    void TurnOfMainCamera()
    {
        sceneCamera = Camera.main;
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(false);
        }
    }

    void GetUniquePlayerName()
    {
        string _ID = PLAYER_TAG + " " + GetComponent<NetworkIdentity>().netId;
        transform.name = _ID;
    }


}
