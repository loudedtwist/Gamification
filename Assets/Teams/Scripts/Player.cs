using UnityEngine;

public class Player : MonoBehaviour
{
    private string playerId;

    private string nickName;

    private int health;

    private Team playerTeam;

    public Team GetMyTeam()
    {
        return playerTeam;
    }

    public string GetPlayerId()
    {
        return playerId;
    }
}
