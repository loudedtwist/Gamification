using UnityEngine;
using UnityEngine.Networking;
using Parse;

public class Player : NetworkBehaviour
{
    private int health;

    private int score;

    private int lvl;

    void Start()
    {
        if (isLocalPlayer)
        {

            string name = ParseUser.CurrentUser.Username; 
            CmdSetUserName(name);
        }
    }

    [Command]
    void CmdSetUserName(string name){  
        transform.name = "NetzPlayer:" + name;
        nickName = name;
    }

    public string PlayerId { get; private set; }

    [SyncVar]
    public string nickName;
}