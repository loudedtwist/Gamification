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
        teams.localPlayer.DecrementPowerUp(PowerTypes.Mirror, () =>
        {
            Debug.LogError("HITTING YOU WITH MIRROR");
            teams.localPlayer.GetComponent<PowerUpMulticaster>().CmdUsePowerUp(new Mirror());
        });
    }

}
