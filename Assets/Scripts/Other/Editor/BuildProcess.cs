using UnityEditor.Build;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using PFS.Assets.Scripts.Views.DebugScreen;

public class BuildProcess : IPreprocessBuild
{
    public int callbackOrder { get { return 0; } }

    public void OnPreprocessBuild(BuildTarget target, string pathH)
    {
        GameObject screen = Resources.Load<GameObject>("Prefabs/UI/DebugScreen");
        if (!screen)
        {
            return;
        }

        DebugView debug = screen.GetComponent<DebugView>();
        if (!debug)
        {
            return;
        }

        string versionBunble;
#if UNITY_STANDALONE || UNITY_WSA
        versionBunble = "Windows BundleVersion: ";
        PlayerSettings.allowedAutorotateToPortrait = false;
        PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
#elif UNITY_ANDROID
        versionBunble = "Android BundleVersion: " + PlayerSettings.Android.bundleVersionCode.ToString();
        PlayerSettings.allowedAutorotateToPortrait = false;
        PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
#elif UNITY_IOS
        versionBunble = "iOs Build: " + PlayerSettings.iOS.buildNumber;
        PlayerSettings.allowedAutorotateToPortrait = false;
        PlayerSettings.allowedAutorotateToPortraitUpsideDown = false;
#endif

        debug.version = string.Format("Version: {0} | {1} | Time: {2}", PlayerSettings.bundleVersion, versionBunble, DateTime.Now.ToString("dd.MM.yyyy HH:mm"));
        File.WriteAllText(Path.Combine(Application.dataPath, "Resources/Version.txt"), PlayerSettings.bundleVersion);
        PrefabUtility.SavePrefabAsset(screen);
    }
}
