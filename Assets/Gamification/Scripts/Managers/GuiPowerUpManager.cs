﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GuiPowerUpManager : SimpleSingleton<GuiPowerUpManager>
{
    protected GuiPowerUpManager()
    {
    }
    // guarantee this will be always a singleton only - can't use the constructor!

    public Text question;

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
}