using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public static class ApplyPrefabsGuid
{
    [MenuItem("Assets/Apply changes for Prefab(s)", false, 1201)]
    private static void ApplyChangesForPrefabs()
    {
        // усі вибілені елементи
        string[] selected = Selection.assetGUIDs;
        foreach (string s in selected)
        {
            // якщо виділений елемент є об'єктом
            if (AssetDatabase.GetMainAssetTypeAtPath(AssetDatabase.GUIDToAssetPath(s)) == typeof(GameObject))
            {
                ApplyChangesForPrefab(s);
            }
            else
            // якщо виділений елемент є папкою, тоді в ній будуть знайдені всі об'єкти
            if (AssetDatabase.GetMainAssetTypeAtPath(AssetDatabase.GUIDToAssetPath(s)) == typeof(DefaultAsset))
            {
                List<string> patch = new List<string>();
                patch.Add(AssetDatabase.GUIDToAssetPath(s));

                string[] guids2 = AssetDatabase.FindAssets(" t:GameObject", patch.ToArray());
                List<UnityEngine.Object> gos = new List<UnityEngine.Object>();
                foreach (string guid in guids2)
                {
                    ApplyChangesForPrefab(guid);
                }
            }
        }
    }

    private static void ApplyChangesForPrefab(string guid)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        GameObject gm = (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        PrefabUtility.SavePrefabAsset(gm);
    }
}
