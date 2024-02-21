using System;
using UnityEngine;

public class ConvertFloatToTime
{
    public static string FormatTime(float seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return $"{time.Minutes:00}:{time.Seconds:00}";
    }
}
