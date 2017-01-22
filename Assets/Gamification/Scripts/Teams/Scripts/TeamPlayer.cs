using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamPlayer : NetworkBehaviour
{ 
    public Team myTeam;

    [SerializeField]
    private Question questionManager;

    [SerializeField]
    private TeamManager teamManager;  
   
    [Command]                               
    public void CmdAddAnswer(Question.Answer answer){  
        questionManager.AddAnswer(answer); 
    }

    void OnEnable(){
        GameObject myObj = GameObject.FindGameObjectWithTag("Question");
        questionManager = myObj.GetComponent<Question>(); 
    }

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