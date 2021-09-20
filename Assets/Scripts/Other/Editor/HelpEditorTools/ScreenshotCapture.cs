using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ScreenshotCapture
{
    private static string folder = "Screenshots", type = ".png";

    [MenuItem("Tools/Create Screenshot &#s", false, 0)]
    public static void CreateScreenshot()
    {
        string screenshotName = string.Format("/Screenshot-{0}-{1}x{2}-{3}", SceneManager.GetActiveScene().name, Screen.width, Screen.height, string.Format("{0:yyyy_MM_dd_HH-mm-ss}", DateTime.Now));

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }
        else
        {
            if (File.Exists(folder + screenshotName + type))
            {
                screenshotName += "-new";
            }
        }

        ScreenCapture.CaptureScreenshot(folder + screenshotName + type);
    }
}