using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public static class ExtensionMethods
{
    public static object ToObject(this string value)
    {
        return Convert.ChangeType(value, typeof(object));
    }
    public static string ToString(this object value)
    {
        return Convert.ToString(value);
    }
    public static GuiManager UI(this MonoBehaviour value)
    {
        return GuiManager.Instance;
    } 
    public static bool Contains(this int[] array, int number){
        for(int i = 0; i<array.Length; i++){
            if (array[i] == number)
                return true;
        }
        return false;
    }

    public static void ChangeStateOfButtons(this IList<Button> buttons, bool state)
    {
        foreach(var b in buttons){
            b.gameObject.SetActive(state);
        } 
    }
    public static void FillWith(this IList<int> array, int num)
    {
        for(int i = 0; i<array.Count; i++){
            array[i] = num;
        } 
    }
    public static String toString(this Question.Answer a)
    { 
            return "{ "+ "\n"
                +"PId: "+a.playerId + "\n"
                +"PName: "+a.playerName + "\n"
                +"Time: "+a.time + "\n"
                +"AnswerCorrect: "+a.correct + "\n"
                +"team: "+a.team + "\n"
                +"}\n"; 
    }
} 
