using UnityEngine;

public class Prefs
{
    public const string k_best_score = "k_best_score";
    public const string k_SoundEnabled = "k_SoundEnabled";
    public const string k_MusicEnabled = "k_MusicEnabled";


    public static int BestScore
    {
        get => PlayerPrefs.GetInt(k_best_score, 0);
        set => PlayerPrefs.SetInt(k_best_score, value);

    }

    public static bool SoundEnabled
    {
        get => PlayerPrefs.GetInt(k_SoundEnabled, 1) == 1;
        set => PlayerPrefs.SetInt(k_SoundEnabled, value ? 1 : 0);
    }

    public static bool MusicEnabled
    {
        get => PlayerPrefs.GetInt(k_MusicEnabled, 1) == 1;
        set => PlayerPrefs.SetInt(k_MusicEnabled, value ? 1 : 0);
    }
}