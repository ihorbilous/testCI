using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;

public class LocalizationCheck : EditorWindow
{
    protected static bool CSVTokens = false;
    protected static bool KTokens = false;
    protected static bool Exclusives = false;
    protected static bool CodebaseExclusives = false;
    protected static bool ShowAllKeys = true;
    protected static string LKeysPlaceholder = "public const string";
    protected Vector2 scrollPosition = new Vector2(0,0);
    protected static string[][] CSVArray;
    protected static string[] KeysTokens;
    protected static List<string> CSVExclusive;
    protected static List<string> LKeysExclusive;
    private static List<string> LCSVList = new List<string>();
    private static List<string> LKeyList = new List<string>();
    private static List<string> LValList = new List<string>();
    private static List<string> foundKeys = new List<string>();


    [MenuItem("Tools/Localization/LocalizationCheck")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        LocalizationCheck window = (LocalizationCheck)EditorWindow.GetWindow(typeof(LocalizationCheck));

        CSVArray = ReadCSV();
        for (int i = 1; i < CSVArray.GetLength(0); i++)
        {
            if (GetVal(i, 0, CSVArray).Trim().StartsWith("ui")){ LCSVList.Add(GetVal(i, 0, CSVArray).Trim()); }
        }
        KeysTokens = ReadLKeys();

        string[] keyValue = new string[2];
        foreach (var a in KeysTokens)
        {
            keyValue = a.Trim(LKeysPlaceholder.ToCharArray()).Split('=');
            KeyValuePair<string, string> LKeyVal = new KeyValuePair<string, string>(keyValue.First().Trim(), keyValue.Last().Trim().Trim("\";".ToCharArray()).Trim('\"'));
            LKeyList.Add(LKeyVal.Key);
            LValList.Add(LKeyVal.Value.Trim());
            
        }
        CSVExclusive = LCSVList.Except(LValList, StringComparer.OrdinalIgnoreCase).ToList();
        LKeysExclusive = LValList.Except(LCSVList, StringComparer.OrdinalIgnoreCase).ToList();
        
        FindStringInCodebase();
        

        window.Show();
    }



    void OnGUI()
    {
        try
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.ExpandHeight(true), GUILayout.ExpandWidth(true));
            
            GUILayout.Label("Localization Check!", EditorStyles.boldLabel);
            ShowAllKeys = EditorGUILayout.Toggle("Show all keys", ShowAllKeys);
            //GET CSV ARRAY


            //CSVTokens = EditorGUILayout.Foldout(CSVTokens, "CSV Tokens");
            //if (CSVTokens)
            //{
            //    for (int i = 1; i < CSVArray.GetLength(0); i++)
            //    {
            //        GUILayout.Label(GetVal(i, 0, CSVArray).Trim() + " : " + GetVal(i, 0, CSVArray).Trim().Length);
            //    }
            //}

            //GUILayout.Label("Localization Keys Tokens", EditorStyles.boldLabel);


            //KTokens = EditorGUILayout.Foldout(KTokens, "Keys Tokens");
            //if (KTokens)
            //{

            //    for (int i = 0; i < LKeyList.Count; i++)
            //    {
            //        EditorGUILayout.SelectableLabel(LKeyList[i] + " : " + LValList[i] + " : " + LValList[i].Length);
            //    }

            //}
            GUILayout.Label("CSV Keys usage", EditorStyles.boldLabel);

            Exclusives = EditorGUILayout.Foldout(Exclusives, "List");
            if (Exclusives)
            { 
                GUIStyle b = new GUIStyle(EditorStyles.label);
                b.richText = true;
                GUIStyle c = new GUIStyle();
                //b.normal.textColor = Color.red;
                //b.font.material.color = Color.red;
               
                foreach (var a in LCSVList)
                {  
                    if (CSVExclusive.Contains(a))
                    {
                        
                        EditorGUILayout.SelectableLabel(a + " <color=red>is not used in LocalizationKeys.cs</color>", b);
                    }
                    else
                    {
                        if (ShowAllKeys)
                        {
                            EditorGUILayout.SelectableLabel(a, b);
                        }
                    }
                }
                //GUILayout.Label("Keys EXCLUSIVE TO LocalizationKeys.cs", EditorStyles.boldLabel);
                //foreach (var a in LKeysExclusive)
                //{
                //    EditorGUILayout.SelectableLabel(a);
                //}
            }
            GUILayout.Label("Localization Keys Usage", EditorStyles.boldLabel);
            CodebaseExclusives = EditorGUILayout.Foldout(CodebaseExclusives, "List");
            if (CodebaseExclusives)
            {
                
                foreach (var a in LKeyList)
                {
                    GUIStyle b = new GUIStyle(GUI.skin.label);
                    b.richText = true;
                    GUIStyle c = new GUIStyle();
                    //b.normal.textColor = Color.red;
                    if (foundKeys.Contains(a))
                    {
                        if (ShowAllKeys)
                        {
                            EditorGUILayout.SelectableLabel(a, b);
                        }
                    }
                    else
                    {
                        EditorGUILayout.SelectableLabel(a + " <color=red>IS NOT USED IN CODEBASE</color>", b);
                    }

                }
                //if (LKeyList.Except(foundKeys).Count() == 0)
                //{
                //    GUILayout.Label("All keys are being used in codebase");
                //}
                //else
                //{
                //    GUILayout.Label($"{LKeyList.Except(foundKeys).Count()} out of {LKeyList.Count}keys are not being used. Remove them!");
                //}
            }
            GUIStyle redStyle = new GUIStyle();
            redStyle.normal.textColor = Color.red;

            GUILayout.Label(" This tool only supports keys formatted like ui.*.* (including multiline translations)\n " +
                "Key arrays/lists are not supported (check them manually!!!)\n" +
                " WARNING: some keys may not be used directly, but are built in runtime via concatenation, \n " +
                "so double-check LocalizationCheck results before removing any keys from table/LocalizationKeys.cs", redStyle);
            EditorGUILayout.EndScrollView();
        }catch(Exception) { Init(); return; }
    }


    public static void FindStringInCodebase()
    {
        string[] csFiles = Directory.GetFiles("Assets/Scripts/", "*.cs", SearchOption.AllDirectories);
        List<string> localLKL = new List<string>();
        localLKL = LKeyList.ToList();
        foreach(string a in csFiles)
        {
           
            foreach (string b in localLKL)
            {
                if (!foundKeys.Contains(b))
                {
                    if (File.ReadAllText(a).Contains(b))
                    {

                        foundKeys.Add(b);
                        continue;
                    }
                }
            }
        }
        
    }

    public static string[][] ReadCSV()
    {
      string[][] m_Data = File
    .ReadLines(Application.dataPath + "/Resources/Localization/Localization.csv")
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Skip(1)
    .Select(line => line.Split(','))
    .ToArray();

        return m_Data;
    }

    public static string GetVal(int row, int column, string[][] csv) => csv[row][column];

    public static string[] ReadLKeys()
    {
        string[] m_Data = File
    .ReadLines(Application.dataPath + "/Scripts/Services/Localization/LocalizationKeys.cs")
    .Where(line => !string.IsNullOrWhiteSpace(line))
    .Where(line => line.TrimStart().StartsWith("public const string"))
    .Skip(1)
    .Select(line => line.TrimStart())
    .ToArray();

        return m_Data;
    }
}
