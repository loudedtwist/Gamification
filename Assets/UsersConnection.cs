using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UsersConnection : MonoBehaviour {

    Color activetedColor;
    Color deactivetedColor;
    public Image []usersT1; 
    public bool []connectedT1; 

    public Image []usersT2; 
    public bool []connectedT2; 

    void OnEnable(){
        connectedT1 = new bool[]{ false, false, false };
        connectedT2 = new bool[]{ false, false, false };
        activetedColor = new Color(131.0f/255, 179.0f/255, 255.0f/255); 
        deactivetedColor = new Color(84.0f/255, 114.0f/255, 159.0f/255);
    }

    public void AddUser(){
        Debug.Log("USER ENTER THE ROOM");
        bool found = false;
        for(int i = 0; i< connectedT1.Length; i++){
            if(connectedT1[i] == false ){
                found = connectedT1[i] = true; 
                usersT1[i].color = activetedColor;
                break;
            }
        }
        if(!found) {
            //show no free slots, game is ready
        }
    } 
    public void ShowUsersConnected(int anz){
        Debug.Log("USER ENTER THE ROOM");
        int found = 0;
        for(int i = 0; i< connectedT1.Length; i++){
            if (found < anz)
            {
                connectedT1[i] = true; 
                usersT1[i].color = activetedColor; 
                found++;
            }
            else
            {
                connectedT1[i] = false; 
                usersT1[i].color = deactivetedColor;
            }
         
        }
        if(found == 0) {
            //show no free slots, game is ready
        }
    } 
}
