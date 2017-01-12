using System;
using UnityEngine;
 
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
} 
