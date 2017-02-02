using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum PowerTypes
{
    Mirror,
    Noiz,
    Boom,
    Initial
}

public class Power
{
    public PowerTypes type;

    public Power()
    {
        this.type = PowerTypes.Initial;
    }

    public Power(PowerTypes type)
    {
        this.type = type;
    }
}

public class Mirror : Power
{
    public Mirror() : base(PowerTypes.Mirror)
    {
    }
}

public class Noiz : Power
{
    public Noiz() : base(PowerTypes.Noiz)
    {
    }
}

public class Boom : Power
{
    public Boom() : base(PowerTypes.Boom)
    {
    }
}

public class PowerUpMulticaster : NetworkBehaviour
{
    private string powerUpName = "Initial Power Up";

    public TeamManager teamManager;

    void Start()
    {
        teamManager = GameObject.FindGameObjectWithTag("TeamManagerTag").GetComponent<TeamManager>();
        if (teamManager == null)
        {
            Debug.LogError("Coulnd't find the TeamManager-Object");
            //GuiManager.Instance.message.For(3).Show("");
        }
    }


    [Command]
    public void CmdUsePowerUp(Power power)
    {
        Debug.LogError("first function call: " + power.GetType().ToString() + " IS CLIENT: " + isClient);

        var teamNr = gameObject.GetComponent<TeamPlayer>().myTeam.teamNr;
        var enemyTeam = teamNr == teamManager.teamA.teamNr ? teamManager.teamB : teamManager.teamA;

        foreach (var p in enemyTeam.Players)
        {
            p.gameObject.GetComponent<PowerUpMulticaster>().RpcNotifyAboutPowerUp(power);
        }
    }

    [ClientRpc]
    public void RpcNotifyAboutPowerUp(Power powerUp)
    {
        Debug.LogError("POWER UP " + powerUp.GetType().ToString() + " IS CLIENT: " + isClient);
        if (!isLocalPlayer) return;
        GuiManager.Instance.message.For(3).Show("YOU WERE FOOLED GUYS");
        switch (powerUp.type)
        {
            case PowerTypes.Mirror:
                GuiPowerUpManager.Instance.SpiegelText();
                break;
            case PowerTypes.Boom:
                break;
            case PowerTypes.Noiz:
                GuiPowerUpManager.Instance.NoizTheScreen();
                break;
            case PowerTypes.Initial:
                break;
            default:
                break;
        }
    }
}