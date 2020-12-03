using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Editor;
using UnityEditor;
using UnityEngine;

public class BuildTool
{
    [MenuItem("BuildTool/SetBuildSettings")]
    public static void SetBuildSettings()
    {
        GameAnalytics.SettingsGA.EmailGA = "di@jollyco.us";
        GameAnalytics.SettingsGA.PasswordGA = "jollyanalytics!!";
        
        GA_SettingsInspector.LoginUser(GameAnalytics.SettingsGA);
        
        PlayerSettings.Android.keystoreName = "user.keystore";
        PlayerSettings.Android.keystorePass = "\"BJQL7nVdTir"; 
        
        PlayerSettings.Android.keyaliasName = "jollymaze";
        PlayerSettings.Android.keyaliasPass = "\"BJQL7nVdTir";

        PlayerSettings.Android.useCustomKeystore = true;
    }
}
