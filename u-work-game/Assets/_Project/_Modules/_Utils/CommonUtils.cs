using UnityEngine;

public class CommonUtils
{
    public static string GetTimeInMinuteAndSeconds(long duration)
    {
        string val = "";
        val = $"{Mathf.FloorToInt(duration / 60).ToString("00")}:{Mathf.FloorToInt(duration % 60).ToString("00")}";

        return val;

    }
}
