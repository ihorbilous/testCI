using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public static class GuidFinder
{
    private static int readCount = 0;
    private static int compareCount = 0;

    private static string popupName;
    private static string description;
    private static float progress;

    private static int guidNum = 0;

    [MenuItem("Assets/Find GUID in Prefabs, Material, Scene, ScriptableObjects", false, 1301)]
    private static void FindGUIDinPrefabsMaterialScenesScriptableObjects()
    {
        var selected = Selection.activeObject;
        string patch = AssetDatabase.GetAssetPath(selected);
        string guids = AssetDatabase.AssetPathToGUID(patch);
        Debug.Log("" + patch + " guid: " + guids + "");

        List<UnityEngine.Object> gos = new List<UnityEngine.Object>();
        foreach (string patchlist in FindObj(guids, "t: Material"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }
        foreach (string patchlist in FindObj(guids, "t: Prefab"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }
        foreach (string patchlist in FindObj(guids, "t: Scene"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }
        foreach (string patchlist in FindObj(guids, "t: ScriptableObject"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }

        if (gos.Count > 0)
        {
            Selection.objects = gos.ToArray();
        }
    }

    [MenuItem("Assets/Find GUID in Prefabs, ScriptableObjects", false, 1302)]
    private static void FindGUIDinPrefabsScriptableObjects()
    {
        var selected = Selection.activeObject;
        string patch = AssetDatabase.GetAssetPath(selected);
        string guids = AssetDatabase.AssetPathToGUID(patch);
        Debug.Log("" + patch + " guid: " + guids + "");

        List<UnityEngine.Object> gos = new List<UnityEngine.Object>();

        foreach (string patchlist in FindObj(guids, "t: Prefab"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }
        foreach (string patchlist in FindObj(guids, "t: ScriptableObject"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }

        if (gos.Count > 0)
        {
            Selection.objects = gos.ToArray();
        }
    }

    [MenuItem("Assets/Find GUID in Prefabs", false, 1303)]
    private static void FindGUIDinPrefabs()
    {
        var selected = Selection.activeObject;
        string patch = AssetDatabase.GetAssetPath(selected);
        string guids = AssetDatabase.AssetPathToGUID(patch);
        Debug.Log("" + patch + " guid: " + guids + "");

        List<UnityEngine.Object> gos = new List<UnityEngine.Object>();

        foreach (string patchlist in FindObj(guids, "t: Prefab"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }

        if (gos.Count > 0)
        {
            Selection.objects = gos.ToArray();
        }
    }

    [MenuItem("Assets/Find GUID in Material", false, 1304)]
    private static void FindGUIDinMaterial()
    {
        var selected = Selection.activeObject;
        string patch = AssetDatabase.GetAssetPath(selected);
        string guids = AssetDatabase.AssetPathToGUID(patch);
        Debug.Log("" + patch + " guid: " + guids + "");

        List<UnityEngine.Object> gos = new List<UnityEngine.Object>();

        foreach (string patchlist in FindObj(guids, "t: Material"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }

        if (gos.Count > 0)
        {
            Selection.objects = gos.ToArray();
        }
    }

    [MenuItem("Assets/Find GUID in Scene", false, 1305)]
    private static void FindGUIDinScene()
    {
        var selected = Selection.activeObject;
        string patch = AssetDatabase.GetAssetPath(selected);
        string guids = AssetDatabase.AssetPathToGUID(patch);
        Debug.Log("<size=12><color=#0000ffff>" + patch + " guid: " + guids + "</color></size>");

        List<UnityEngine.Object> gos = new List<UnityEngine.Object>();

        foreach (string patchlist in FindObj(guids, "t: Scene"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }

        if (gos.Count > 0)
        {
            Selection.objects = gos.ToArray();
        }
    }

    [MenuItem("Assets/Find GUID in ScriptableObjects", false, 1306)]
    private static void FindGUIDinScriptableObjects()
    {
        var selected = Selection.activeObject;
        string patch = AssetDatabase.GetAssetPath(selected);
        string guids = AssetDatabase.AssetPathToGUID(patch);
        Debug.Log("<size=12><color=#0000ffff>" + patch + " guid: " + guids + "</color></size>");

        List<UnityEngine.Object> gos = new List<UnityEngine.Object>();

        foreach (string patchlist in FindObj(guids, "t: ScriptableObject"))
        {
            gos.Add(AssetDatabase.LoadAssetAtPath(patchlist, typeof(UnityEngine.Object)));
        }

        if (gos.Count > 0)
        {
            Selection.objects = gos.ToArray();
        }
    }

    //-------------------DELETE TEXTURES
    [MenuItem("Assets/Delete unused Textures in folder and subfolders (in Prefabs)", false, 1401)]
    private static void FindGUIDinPrefabsDelete()
    {
        FindAndDelete(" t:Texture", new string[] { "t: Prefab",  });
    }

    [MenuItem("Assets/Delete unused Textures in folder and subfolders (in Prefabs, ScriptableObject)", false, 1402)]
    private static void FindGUIDinPrefabsScriptableObjectsDelete()
    {
        FindAndDelete(" t:Texture", new string[] {  "t: Prefab", "t: ScriptableObject" });
    }

    [MenuItem("Assets/Delete unused Textures in folder and subfolders (in Prefabs, ScriptableObject, Material, Animations)", false, 1403)]
    private static void FindGUIDinPrefabsMaterialScriptableObjectsDelete()
    {
        FindAndDelete(" t:Texture", new string[] { "t: Material", "t: Prefab",  "t: ScriptableObject", "t:AnimationClip" });
    }
    //--------------------

    //-------------------DELETE PREFABS
    [MenuItem("Assets/Delete unused Prefabs in folder and subfolders (in Prefabs)", false, 1601)]
    private static void FindPrebabsGUIDinPrefabsDelete()
    {
        FindAndDelete(" t:Prefab", new string[] { "t: Prefab", });
    }

    [MenuItem("Assets/Delete unused Prefabs in folder and subfolders (in Prefabs, ScriptableObject)", false, 1602)]
    private static void FindPrefabsGUIDinPrefabsScriptableObjectsDelete()
    {
        FindAndDelete(" t:Prefab", new string[] { "t: Prefab", "t: ScriptableObject" });
    }
    //--------------------

    private static async void FindAndDelete(string findWhatFilter, string [] findWhereFilters)
    {
        var selected = Selection.activeObject;
        List<string> paths = new List<string>
        {
            AssetDatabase.GetAssetPath(selected)
        };

        string[] guids = AssetDatabase.FindAssets(findWhatFilter, paths.ToArray());

        var usedObjecQuids = await FindUsedObjects(guids, findWhereFilters);
        var unusedGuids = guids.ToList().Except(usedObjecQuids);

        ShowProgressPopup(popupName, description = "Deleting", progress);
        foreach (var unusedGuid in unusedGuids)
        {
            var path = AssetDatabase.GUIDToAssetPath(unusedGuid);
            AssetDatabase.DeleteAsset(path);
            Debug.Log($"<size=14> Asset <color=green> >>{path}<<</color> was deleted </size>");
        }
        EditorUtility.ClearProgressBar();
    }

    #region Help Methods
    private static List<string> ret = new List<string>();
   
    public static List<string> FindObj(string guids, string findAssetsText)
    {
        Debug.Log("3");
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
        Debug.Log("4");
        return ret;
    }

    public static async Task<List<string>> FindUsedObjects(string[] guids, string[] searchFilters)
    {
        var time = DateTime.Now;
        ShowProgressPopup(popupName = "Deleting unused textures from folder", description = "Progressing" , progress = 0);
        readCount = 0;
        var usedObjectGuids = new List<string>();
        //-----Collect list of filters GUID
        List<string> searchFilterGuids = new List<string>();
        foreach (var searchFilter in searchFilters)
        {
            searchFilterGuids.AddRange(AssetDatabase.FindAssets(searchFilter));
        }

        //-----Collect list of filters paths
        List<string> filterPaths = new List<string>();
        foreach (var searchFilterGuid in searchFilterGuids)
        {
            filterPaths.Add(AssetDatabase.GUIDToAssetPath(searchFilterGuid));
        }

        //-----Collect list of filters files text
        string dataPath = Application.dataPath;
        ShowProgressPopup(popupName, description = "Check files", progress);
        List<string> filterFileLines = new List<string>();
        foreach (var filterPath in filterPaths)
        {
              ReadTheFileAsync(filterPath, dataPath, filterFileLines);
        }

        while (readCount > 0)
        {
            ShowProgressPopup(popupName, "Waiting to finish reading all files", progress);
            await Task.Delay(50);
        }

        compareCount = 0;
        guidNum = 0;
        foreach (var guid in guids)
        {
            CompareFilesAsync(filterFileLines, guid, usedObjectGuids);
            //foreach (string filterFileLine in filterFileLines)
            //{
            //    if (filterFileLine.Contains(guid))
            //    {
            //        usedObjectGuids.Add(guid);
            //        break;
            //    }
            //}
        }

        while (compareCount > 0)
        {
            ShowProgressPopup(popupName, $"Check file: [{guidNum} from {guids.Length}]", progress);
            await Task.Delay(50);
        }

        var time2 = DateTime.Now;
        Debug.Log(time2 - time);
        EditorUtility.ClearProgressBar();
        return usedObjectGuids;
    }

    private static async void CompareFilesAsync(List<string> filterFileLines, string guid, List<string> usedObjectGuids)
    {
        compareCount++;
        await Task.Run(() =>
        {
            foreach (string filterFileLine in filterFileLines)
            {
                if (filterFileLine.Contains(guid))
                {
                    usedObjectGuids.Add(guid);
                    break;
                }
            }
            
        }
        );
        guidNum++;
        compareCount--;
    }

    private static async void ReadTheFileAsync(string assetPath, string dataPath, List<string> filterFileLines)
    {
        readCount++;
        try
        {
            using (StreamReader sr = new StreamReader(dataPath + "/../" + assetPath))
            {
                string line = await sr.ReadToEndAsync();
                if (!string.IsNullOrEmpty(line))
                {
                    filterFileLines.Add(line);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        float currentCount = filterFileLines.Count;
        float maxCount = readCount + filterFileLines.Count;
        progress = ((100f * currentCount)/maxCount)/100f;
        ShowProgressPopup(popupName, description + $" [{filterFileLines.Count} from {readCount + filterFileLines.Count}] ", progress);
        readCount--;
    }

    static private void ShowProgressPopup(string popupName, string description, float progress)
    {
        EditorUtility.DisplayProgressBar(popupName, description, progress);
    }
    #endregion
}
