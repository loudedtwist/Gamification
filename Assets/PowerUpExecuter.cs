using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpExecuter : MonoBehaviour {

    public TeamManager teams;


    void Start()
    {
        //Do();
    }

    public void Do()
    {
        teams.localPlayer.GetComponent<PowerUpMulticaster>().CmdUsePowerUp(new Mirror());
    }
}
