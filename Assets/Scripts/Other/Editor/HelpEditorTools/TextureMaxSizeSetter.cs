using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class SetTextureMaxSize : EditorWindow
{
    public int maxSizeProc = 100;
    private static string last = "";

    private void OnGUI()
    {
        LoadWindowSetMaxSize();
    }

    [MenuItem("Assets/Set texture size", false, 1101)]
    private static void ShowWindowSetMaxSize()
    {
        EditorWindow.GetWindow(typeof(SetTextureMaxSize), true, "Set texture size");
    }

    private void LoadWindowSetMaxSize()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Current size percent", GUILayout.Width(150));
        maxSizeProc = EditorGUILayout.IntField(getCurrentSize(maxSizeProc), GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Apply new size"))
        {
            MaxSizetoRealSize(maxSizeProc);
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Аналіз виділених елементів та подальше знаходження текстур і встановлення заданих розмірів у відсотковому значені
    /// </summary>
    /// <param name="sizeProc"></param>
    private static void MaxSizetoRealSize(int sizeProc)
    {
        // усі вибілені елементи
        string[] selected = Selection.assetGUIDs;
        foreach (string s in selected)
        {
            // якщо виділений елемент є текстурою
            if (AssetDatabase.GetMainAssetTypeAtPath(AssetDatabase.GUIDToAssetPath(s)) == typeof(Texture2D))
            {
                string patch = AssetDatabase.GUIDToAssetPath(s);

                setSize(patch, sizeProc);
            }
            else
            // якщо виділений елемент є папкою, тоді в ній будуть знайдені всі текстури
            if (AssetDatabase.GetMainAssetTypeAtPath(AssetDatabase.GUIDToAssetPath(s)) == typeof(DefaultAsset))
            {
                List<string> patch = new List<string>
                {
                    AssetDatabase.GUIDToAssetPath(s)
                };

                string[] guids2 = AssetDatabase.FindAssets(" t:Texture", patch.ToArray());
                List<Object> gos = new List<Object>();
                foreach (string guid in guids2)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);

                    setSize(assetPath, sizeProc);
                }
            }
            else
            {
                //Debug.LogError("Wrong type. Need to Texture2D for set size");
            }
        }
    }

    /// <summary>
    /// Отримання поточного розміру текстури у відсотках. Можна дізнатися тільки для одного виділеного елементу
    /// </summary>
    /// <param name="currentSize"></param>
    /// <returns></returns>
    private static int getCurrentSize(int currentSize)
    {
        Object selected = Selection.activeObject;
        if (selected.GetType() == typeof(Texture2D))
        {
            string patch = AssetDatabase.GetAssetPath(selected);
            if (last != patch)
            {
                //last = selected.name;
                //string patch = AssetDatabase.GetAssetPath(selected);
                last = patch;

                TextureImporter tImporter = AssetImporter.GetAtPath(patch) as TextureImporter;
                Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(patch, typeof(Texture2D));
                int width = 0, height = 0;
                object[] args = new object[2] { 0, 0 };
                MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
                mi.Invoke(tImporter, args);
                width = (int)args[0];
                height = (int)args[1];

                return (int)System.Math.Round(texture.width * 100f / width);
            }
            else
            {
                return currentSize;
            }
        }
        else
        {
            //Debug.LogError("Wrong type. Need to Texture2D for get size");
            return currentSize;
        }
    }

    /// <summary>
    /// Встановлення заданого розміру у відсотках для вказаної текстури
    /// </summary>
    /// <param name="assetPath"></param>
    /// <param name="sizeProc"></param>
    private static void setSize(string assetPath, int sizeProc)
    {
        TextureImporter tImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;

        int width = 0, height = 0;
        object[] args = new object[2] { 0, 0 };
        MethodInfo mi = typeof(TextureImporter).GetMethod("GetWidthAndHeight", BindingFlags.NonPublic | BindingFlags.Instance);
        mi.Invoke(tImporter, args);
        width = (int)args[0];
        height = (int)args[1];
        int realSize = 0;
        if (width >= height)
        {
            realSize = width;
        }
        else
        {
            realSize = height;
        }

        tImporter.maxTextureSize = (int)System.Math.Round(realSize * sizeProc / 100f);

        AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);
    }
}
