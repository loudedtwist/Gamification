using UnityEngine;
using Parse;

public class Player : MonoBehaviour
{
    private int health;

    private int score;

    private int lvl;

    void Start()
    {
        transform.name = ParseUser.CurrentUser.Username;
        NickName = ParseUser.CurrentUser.Username;
    }

    public string PlayerId { get; private set; }

    public string NickName { get; set; }
}