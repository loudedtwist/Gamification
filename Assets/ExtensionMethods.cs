using System;
 
public static class ExtensionMethods
{
    public static Object ToObject(this string value)
    {
        return Convert.ChangeType(value, typeof(Object));
    }
    public static string ToString(this object value)
    {
        return Convert.ToString(value);
    }
} 
