using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
using UnityEngine;
//using UnityEditor.iOS.Xcode;



//#if UNITY_IOS
//using AppleAuth.Editor;
//using UnityEditor.iOS.Xcode;
//#endif

public static class BuildScript
{
    public static void BuildForUWP()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/main.unity" };
        buildPlayerOptions.locationPathName = "build";
        buildPlayerOptions.target = BuildTarget.WSAPlayer;
        buildPlayerOptions.options = BuildOptions.None;
        EditorUserBuildSettings.wsaUWPBuildType = WSAUWPBuildType.XAML;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
    {
        if (buildTarget == BuildTarget.iOS)
        {
//#if UNITY_IOS
//            var projectPath = PBXProject.GetPBXProjectPath(buildPath);

//            var project = new PBXProject();
//            project.ReadFromString(System.IO.File.ReadAllText(projectPath));
//            string frameworkGuid = project.GetUnityFrameworkTargetGuid();
//            var iphoneGuid = project.GetUnityMainTargetGuid();

//            project.AddFrameworkToProject(iphoneGuid, "StoreKit.framework", false); // In-App Purchase
//            project.AddBuildProperty(frameworkGuid, "OTHER_LDFLAGS", "-lsqlite3");
//            project.WriteToFile(projectPath);

//            var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", null, project.GetUnityMainTargetGuid());
//            manager.AddSignInWithAppleWithCompatibility(frameworkGuid);
//            manager.AddInAppPurchase(); // AddFrameworkToProject() With StoreKit.framework
//            manager.WriteToFile();
//#endif
        }
        else if (buildTarget == BuildTarget.StandaloneOSX)
        {
//#if UNITY_STANDALONE_OSX
//            //if (EditorUserBuildSettings.GetPlatformSettings(BuildPipeline.GetBuildTargetName(BuildTarget.StandaloneOSX), "CreateXcodeProject").ToLower() == "true")
//            //{
//                var projectPath = PBXProject.GetPBXProjectPath(buildPath);
//                var project = new PBXProject();
//                string frameworkGuid = project.GetUnityFrameworkTargetGuid();
//                project.AddBuildProperty(frameworkGuid, "OTHER_CODE_SIGN_FLAGS", "--deep --force");
//                var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", null, project.GetUnityMainTargetGuid());
//                project.WriteToFile(projectPath);
//                manager.WriteToFile();

//                Regex plistIdentifierRegex = new Regex(@"<key>CFBundleIdentifier<\/key>\D{2}<string>(.*)<\/string>");
//                Regex plistNameRegex = new Regex(@"<key>CFBundleName<\/key>\D{2}<string>(.*)<\/string>");


//                //////
//                //info.plist bundle fix
//                //////
//                string str;
//                using (StreamReader reader = File.OpenText(new DirectoryInfo(buildPath).Parent +
//                    "/Pickatale For School/PlugIns/unitypurchasing.bundle/Contents/info.plist"))
//                {
//                    str = reader.ReadToEnd();
//                    MatchCollection matches = plistIdentifierRegex.Matches(str);

//                    str = plistIdentifierRegex.Replace(str, "<key>CFBundleIdentifier</key>\n\t<string>com.pickatale.pickataleschool." + plistNameRegex.Match(str).Groups[1].Value + "</string>");
//                    UnityEngine.Debug.Log(str);
//                }

//                using (System.IO.StreamWriter file = new System.IO.StreamWriter(new DirectoryInfo(buildPath).Parent +
//                    "/Pickatale For School/PlugIns/unitypurchasing.bundle/Contents/info.plist"))
//                {
//                    file.Write(str);
//                }

//                using (StreamReader reader = File.OpenText(new DirectoryInfo(buildPath).Parent +
//                    "/Pickatale For School/PlugIns/MacOSAppleAuthManager.bundle/Contents/info.plist"))
//                {
//                    str = reader.ReadToEnd();
//                    MatchCollection matches = plistIdentifierRegex.Matches(str);

//                    str = plistIdentifierRegex.Replace(str, "<key>CFBundleIdentifier</key>\n\t<string>com.pickatale.pickataleschool." + plistNameRegex.Match(str).Groups[1].Value + "</string>");
//                    UnityEngine.Debug.Log(str);
//                }

//                using (System.IO.StreamWriter file = new System.IO.StreamWriter(new DirectoryInfo(buildPath).Parent +
//                    "/Pickatale For School/PlugIns/MacOSAppleAuthManager.bundle/Contents/info.plist"))
//                {
//                    file.Write(str);
//                }

//                using (StreamReader reader = File.OpenText(new DirectoryInfo(buildPath).Parent +
//                    "/Pickatale For School/PlugIns/UniWebView.bundle/Contents/info.plist"))
//                {
//                    str = reader.ReadToEnd();
//                    MatchCollection matches = plistIdentifierRegex.Matches(str);

//                    str = plistIdentifierRegex.Replace(str, "<key>CFBundleIdentifier</key>\n\t<string>com.pickatale.pickataleschool." + plistNameRegex.Match(str).Groups[1].Value + "</string>");
//                    UnityEngine.Debug.Log(str);
//                }

//                using (System.IO.StreamWriter file = new System.IO.StreamWriter(new DirectoryInfo(buildPath).Parent +
//                    "/Pickatale For School/PlugIns/UniWebView.bundle/Contents/info.plist"))
//                {
//                    file.Write(str);
//                }

//                string macStylePath = buildPath + "/project.pbxproj";

//                string s = File.ReadAllText(macStylePath);
//                s = s.Replace(
//                    "CODE_SIGN_STYLE = Automatic;",
//                    "CODE_SIGN_STYLE = Automatic;\n\t\t\t\t\"OTHER_CODE_SIGN_FLAGS[sdk = *]\" = \"--deep--force\";");

//                File.Delete(macStylePath);
//                File.WriteAllText(macStylePath, s);

//            //}
//#endif
        }
    }
}