using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPage : MonoBehaviour
{

    public Text teamALabel;

    public Text teamBLabel;

    public Text deinePunkteLabel;

    public Team teamA;

    public Team teamB;

    public void OnEnable()
    {
        teamALabel.text += " punkte";
        teamBLabel.text += " punkte";
        deinePunkteLabel.text += " punkte";
    }

}
