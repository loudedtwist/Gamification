using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPlayer : MonoBehaviour
{ 
    public Team myTeam;

    [SerializeField]
    private TeamManager teamManager; 

    void Start()
    {
        teamManager = GameObject.FindGameObjectWithTag("TeamManagerTag").GetComponent<TeamManager>();
        myTeam = teamManager.SignUpPlayerToTeam(this);
        if(myTeam == null){
            //TODO React to full room -> show htw map , 
            Debug.LogError("Can't join the lobby, the room is full");
        }
    }

    private void OnDestroy()
    {
        teamManager.UnsignPlayerFromTeam(this);
    }
}