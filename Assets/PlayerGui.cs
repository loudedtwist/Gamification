using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerGui : NetworkBehaviour {  
 
    [Client]
    public void GoToWaitingForUserPage(){
        if(isLocalPlayer){
            GuiManager.Instance.ShowWaitingForUserPage();
        }
    }
 
}
