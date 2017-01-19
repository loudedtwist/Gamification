using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UsersConnection : MonoBehaviour
{
    public Image[] usersT1;

    public Image[] usersT2;

    public void ShowUsersConnected(Team team)
    {
        Debug.Log("USER ENTER THE ROOM");
        int found = 0;
        int anz = team.Players.Count;
        for (int i = 0; i < usersT1.Length; i++)
        {
            if (found < anz)
            {
                if(team.teamNr == 1 ) usersT1[i].color = team.teamColorConnected;
                if(team.teamNr == 2 ) usersT2[i].color = team.teamColorConnected;
                found++;
            }
            else
            {
                if(team.teamNr == 1 ) usersT1[i].color = team.teamColorDisconnected;
                if(team.teamNr == 2 ) usersT2[i].color = team.teamColorDisconnected;
            }
        }
        if (found == 0)
        {
            //show no free slots, game is ready
        }
    }
}