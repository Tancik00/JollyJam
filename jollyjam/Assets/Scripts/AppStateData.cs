using UnityEngine;

public class AppStateData 
{
    public static bool IsPause;
    public static bool IsRestartButtonPressed;
    
    public static bool isSoundOn
    {
        get => PlayerPrefs.GetInt("soundOn",1)==1;
        set => PlayerPrefs.SetInt("soundOn", value?1:0);
    } 
    
    public static bool isMusicOn
    {
        get => PlayerPrefs.GetInt("musicOn",1)==1;
        set => PlayerPrefs.SetInt("musicOn", value?1:0);
    } 
    
    public static bool isVibrationOn
    {
        get => PlayerPrefs.GetInt("vibrationOn",1)==1;
        set => PlayerPrefs.SetInt("vibrationOn", value?1:0);
    }
}
