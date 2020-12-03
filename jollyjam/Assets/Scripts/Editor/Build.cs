//nice ref
//https://github.com/PimDeWitte/UnityAutoIncrementBuildVersion/blob/master/AutoIncrementBuildVersion.cs#L50

#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using Google;
using UnityEngine;
using UnityEditor;
using UnityEditor.Connect;
using UnityEditor.CrashReporting;
using UnityEditor.Build.Reporting;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;

public class AndroidBuilder : Build
{
    public static void ProductionBuild()
    {
        FillSdkPaths();

        var targetGroup = BuildTargetGroup.Android;
        var target = BuildTarget.Android;
        
        EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
        EditorUserBuildSettings.buildAppBundle = true;
        
        //CrashReportingSettings.enabled = true;
        
#if UNITY_ANDROID
        
        //GooglePlayServices.PlayServicesResolver.Resolver.SetAutomaticResolutionEnabled(true);
        //GooglePlayServices.PlayServicesResolver.MenuForceResolve();
#endif
        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();

        PlayerSettings.SetScriptingDefineSymbolsForGroup(targetGroup, "DREAMTECK_SPLINES;CLOUDONCE_GOOGLE");
        PlayerSettings.SetApiCompatibilityLevel(targetGroup, ApiCompatibilityLevel.NET_4_6);
        PlayerSettings.SetApplicationIdentifier(targetGroup, GetArg("-applicationIdentifier"));
        PlayerSettings.bundleVersion = GetArg("-version");
        PlayerSettings.Android.bundleVersionCode = int.Parse(GetArg("-buildNumber"));
        
        BuildTool.SetBuildSettings();
        
       /* PlayerSettings.Android.keystoreName = GetArg("-keystoreName");
        PlayerSettings.Android.keystorePass = GetArg("-keystorePass");
        PlayerSettings.Android.keyaliasName = GetArg("-keyaliasName");
        PlayerSettings.Android.keyaliasPass = GetArg("-keyaliasPass");*/
       
#if UNITY_ANDROID
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;

#endif
        // -keystoreName ${KEYSTORE_NAME}  -keystorePass ${KEYSTORE_PASS}  -keyaliasName ${KEYALIAS_NAME} -keyaliasPass ${KEYALIAS_PASS}
        
        AssetDatabase.SaveAssets();


        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

        buildPlayerOptions.scenes = GetAllLevels();
        buildPlayerOptions.locationPathName = GetArg("-locationPathName");
        buildPlayerOptions.target = target;
        buildPlayerOptions.options = BuildOptions.None;
        
        BuildWithOptions(buildPlayerOptions);
    }
}

public class Build
{
    // Helper function for getting the command line arguments
    public static string GetArg(string name)
    {
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return "???";
    }


    public static void DoAndoirdBuildAndRun()
    {
        Build.DoBuild(buildOptions: BuildOptions.AutoRunPlayer);
    }

    public static void DoAndoirdBuild()
    {
        DoBuild();
    }

    protected static void BuildWithOptions(BuildPlayerOptions options)
    {
        PlayerSettings.SplashScreen.show = false;
        var res = BuildPipeline.BuildPlayer(options);
        if (res.summary.result != BuildResult.Succeeded)
        {
            throw new Exception("BuildPlayer failure: " + res.summary);
        }
    }
    
    public static void DoBuild(string name = null, string[] scenes = null, BuildTarget target = BuildTarget.Android,
        BuildOptions buildOptions = BuildOptions.None)
    {

        FillSdkPaths();

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        FillScenes(scenes, ref buildPlayerOptions);
        FillDestination(name, target, ref buildPlayerOptions);
        buildPlayerOptions.target = target;
        buildPlayerOptions.options = buildOptions | BuildOptions.StrictMode;

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    
        AssetDatabase.SaveAssets();
    }

    protected static void FillSdkPaths()
    {
        var sdkPath = GetArg("-AndroidSdkRoot");
        var jdkPath = GetArg("-JdkPath");
        var ndkPath = GetArg("-AndroidNdkRoot");
        
        Debug.Log("AndroidSdkRoot" + sdkPath);
        Debug.Log("JdkPath" + jdkPath);
        Debug.Log("AndroidNdkRoot" + ndkPath);
        
        if (sdkPath != null && jdkPath != null && ndkPath != null)
        {
            EditorPrefs.SetString("AndroidSdkRoot", sdkPath);
            EditorPrefs.SetString("JdkPath", jdkPath);
            EditorPrefs.SetString("AndroidNdkRoot", ndkPath);
        }
        else
        {
            throw new Exception("Not valid path");
        }
    }

    private static void FillDestination(string name, BuildTarget target, ref BuildPlayerOptions buildPlayerOptions)
    {
        string extension = string.Empty;
        switch (target)
        {
           case BuildTarget.Android:
                extension = ".aab";
                break;
            default:
                throw new ArgumentOutOfRangeException("target", target, null);
        }

        var fileName = GetArg("-Name");
        
        var destination = GetArg("-BuildDestination");
        if (destination == null)
        {
            string path = string.Empty;
            path = Application.dataPath;
            string path1 = path.Substring(0, path.LastIndexOf('/'));
            destination = path1 + "/";
        }

        buildPlayerOptions.locationPathName = destination + fileName + extension;
    }

    private static void FillScenes(string[] scenes, ref BuildPlayerOptions buildPlayerOptions)
    {
        if (scenes == null)
            buildPlayerOptions.scenes = GetAllLevels();
        else
            buildPlayerOptions.scenes = scenes;
    }

    protected static string[] GetAllLevels()
    {
        return (from scene in EditorBuildSettings.scenes where scene.enabled select scene.path).ToArray();
    }
}
#endif