using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpExecuter : MonoBehaviour {

    public TeamManager teams;

    void Start()
    {
        //Do();
    }

    public void DoMirror()
    {
        teams.localPlayer.DecrementPowerUp(PowerTypes.Mirror, () =>
        {
            Debug.LogError("HITTING YOU WITH MIRROR");
            teams.localPlayer.GetComponent<PowerUpMulticaster>().CmdUsePowerUp(new Mirror());
        });
    }

    public void DoDirt()
    {
        teams.localPlayer.DecrementPowerUp(PowerTypes.Noiz, () =>
        {
            Debug.LogError("HITTING YOU WITH NOIZ");
            teams.localPlayer.GetComponent<PowerUpMulticaster>().CmdUsePowerUp(new Noiz());
        });
    }

}
