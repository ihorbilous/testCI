using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TextureReadWriteModifier
{
    //
    [MenuItem("Assets/Textures/Find all Texture in folder end disable read-write", false, 1509)]
    private static void DisableReadWrite()
    {
        var selected = Selection.activeObject;
        List<string> patch = new List<string>
        {
            AssetDatabase.GetAssetPath(selected)
        };

        string[] guids2 = AssetDatabase.FindAssets(" t:Texture", patch.ToArray());
        List<UnityEngine.Object> gos = new List<UnityEngine.Object>();
        foreach (string guid in guids2)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log(assetPath);

            TextureImporter tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture2D));
            tImporter.isReadable = false;
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
        }
    }

    private static List<string> ret = new List<string>();
    public static List<string> FindObj(string guids, string findAssetsText)
    {
        ret = new List<string>();
        string[] guids2 = AssetDatabase.FindAssets(findAssetsText);
        foreach (string guid in guids2)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            try
            {
                using (StreamReader sr = new StreamReader(Application.dataPath + "/../" + assetPath))
                {
                    string line = sr.ReadToEnd();
                    if (line.Contains(guids))
                    {
                        Debug.Log("<size=14><color=#0000ffff> Found file::" + assetPath + "</color></size>");
                        ret.Add(assetPath);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        if (ret.Count < 1)
        {
            Debug.Log("<size=14><color=#ff0f0fff> NOT Found files</color></size>");
        }
        return ret;
    }
}
