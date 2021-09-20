using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TextureMaxSizeModifier
{
    [MenuItem("Assets/Textures/Find all Texture in folder end SCALE DOWN max size", false, 1506)]
    private static void FindTextureDown()
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

            //gos.Add(AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)));


            TextureImporter tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            tImporter.mipmapEnabled = true;
            int prevSize = tImporter.maxTextureSize;
            tImporter.maxTextureSize = prevSize / 2;
            Debug.Log("From " + prevSize + " to " + prevSize / 2);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

        }
        //if (gos.Count > 0)
        //{
        //    Selection.objects = gos.ToArray();
        //}
    }

    [MenuItem("Assets/Textures/Find all Texture in folder end SCALE UP max size", false, 1507)]
    private static void FindTextureUP()
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

            //gos.Add(AssetDatabase.LoadAssetAtPath(assetPath, typeof(UnityEngine.Object)));


            TextureImporter tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            tImporter.mipmapEnabled = true;
            int prevSize = tImporter.maxTextureSize;
            tImporter.maxTextureSize = prevSize * 2;
            Debug.Log("From " + prevSize + " to " + prevSize * 2);
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

        }
        //if (gos.Count > 0)
        //{
        //    Selection.objects = gos.ToArray();
        //}
    }

    [MenuItem("Assets/Textures/Find all Texture in folder and change MaxSize to the nearest LOWER value of Real Size", false, 1508)]
    private static void MaxSizetoRealSize()
    {
        var selected = Selection.activeObject;
        List<string> patch = new List<string>
        {
            AssetDatabase.GetAssetPath(selected)
        };

        string[] guids = AssetDatabase.FindAssets(" t:Texture", patch.ToArray());
        List<UnityEngine.Object> objects = new List<UnityEngine.Object>();
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Debug.Log(assetPath);

            TextureImporter tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath, typeof(Texture2D));
            tImporter.maxTextureSize = 8192; //set Max value to see non-catted real size 
            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
            //tImporter.mipmapEnabled = true;
            int realSize = 0;
            if (texture.width >= texture.height)
            {
                realSize = texture.width;
                Debug.Log("realSize 1 = " + realSize);
            }
            else
            {
                realSize = texture.height;
                Debug.Log("realSize 2 = " + realSize);
            }
            if (realSize >= 8192)
            {
                tImporter.maxTextureSize = 8192;
            }
            else if (realSize >= 4096)
            {
                tImporter.maxTextureSize = 4096;
            }
            else if (realSize >= 2048)
            {
                tImporter.maxTextureSize = 2048;
            }
            else if (realSize >= 1024)
            {
                tImporter.maxTextureSize = 1024;
            }
            else if (realSize >= 512)
            {
                tImporter.maxTextureSize = 512;
            }
            else if (realSize >= 256)
            {
                tImporter.maxTextureSize = 256;
            }
            else if (realSize >= 128)
            {
                tImporter.maxTextureSize = 128;
            }
            else if (realSize >= 64)
            {
                tImporter.maxTextureSize = 64;
            }
            else if (realSize >= 32)
            {
                tImporter.maxTextureSize = 32;
            }

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

        }
    }
}
