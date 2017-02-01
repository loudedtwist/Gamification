using System.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class GuiPowerUpManager : SimpleSingleton<GuiPowerUpManager>
{
    protected GuiPowerUpManager()
    {
    }
    // guarantee this will be always a singleton only - can't use the constructor!

    public Text question;



    [SerializeField]
    private Text mirrorAnzahl;

    [SerializeField]
    private GameObject dirtLayer;

    public void SetMirrorAnzahlText(int anz)
    {
        mirrorAnzahl.text = "Spiegel: " + anz;
    }


    public void SpiegelText()
    {
        var oldText = question.text;
        question.text = Reverse(oldText);
    }

    private static string Reverse( string s )
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse( charArray );
        return new string( charArray );
    }

    public void NoizTheScreen()
    {
        dirtLayer.SetActive(true);
        Invoke("UndoNoiz", 5.0f);
    }

    private void UndoNoiz()
    {
        dirtLayer.SetActive(false);
    }
}