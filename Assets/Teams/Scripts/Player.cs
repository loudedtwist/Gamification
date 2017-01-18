using UnityEngine;
using Parse;

public class Player : MonoBehaviour
{
    private string playerId;

    private string nickName;

    private int health;

    // Use this for initialization
    void Start () {
        transform.name = ParseUser.CurrentUser.Username;
        nickName = ParseUser.CurrentUser.Username;
    }

    public string GetPlayerId()
    {
        return playerId;
    }
}
