using System.Collections;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
//using UnityGoogleDrive;

namespace EditorCoroutines
{
    public class LocalizationUpdator : EditorWindow
    {
        private const string fileId = "18MiPNJX9_bzIeU5zfNdCsVKy1bLDzMVmdF-ASZyPUsA";
        private static string result = string.Empty;


        //---------------Localization------------------------------------
        [MenuItem("Tools/Localization/UpdateLocalization")]
        static void SetLocalization()
        {
            Debug.Log("Update localization start. Wait...");

            CoroutineClass coroutine = new CoroutineClass();
            //coroutine.StartCoroutine(SaveLocalization());
            SaveLocalization();
        }

        public static async void SaveLocalization()
        {
            ////GoogleDriveFiles.ExportRequest request = GoogleDriveFiles.Export(fileId, "text/csv");
            //await request.Send();
            //if (!request.IsError && request.ResponseData != null && request.IsDone)
            //{
            //    result = Encoding.UTF8.GetString(request.ResponseData.Content);
            //    string path = Application.dataPath + "/Resources/Localization/Localization.csv";
            //    File.WriteAllText(path, result);
            //    Debug.Log("Update localization done");
            //}
            //else
            //{
            //    Debug.LogError("Download Localization ERROR " + request.Error);
            //}

            AssetDatabase.Refresh();
            //string url = "https://docs.google.com/spreadsheets/d/18MiPNJX9_bzIeU5zfNdCsVKy1bLDzMVmdF-ASZyPUsA/export?format=csv&id=106Y3i_F7U1OHAR_hGVbhHXiSooJ1O_zr6Oj49c2JRzg&gid=0";
            //UnityWebRequest www = UnityWebRequest.Get(url);
            //yield return www.SendWebRequest();

            //if (www.isNetworkError || www.isHttpError)
            //{
            //    Debug.LogError("Download Localization ERROR " + www.error);
            //}
            //else
            //{
            //    Debug.Log("Update localization done");
            //    string path = Application.dataPath + "/Resources/Localization/Localization.csv";
            //    File.WriteAllBytes(path, www.downloadHandler.data);

            //    AssetDatabase.Refresh();
            //}
        }
        //-----------------------------------------------------------end

        //-----------------------------------------------------------LEGACY (NO OAUTH) Localization Update. Remove when out of use
        [MenuItem("Tools/Localization/UpdateLocalization (Legacy)")]
        static void SetLocalizationLegacy()
        {
            Debug.Log("Update localization start. Wait...");

            CoroutineClass coroutine = new CoroutineClass();
            coroutine.StartCoroutine(SaveLocalizationLegacy());
            
        }
        public static IEnumerator SaveLocalizationLegacy()
        {

            string url = "https://docs.google.com/spreadsheets/d/18MiPNJX9_bzIeU5zfNdCsVKy1bLDzMVmdF-ASZyPUsA/export?format=csv&id=106Y3i_F7U1OHAR_hGVbhHXiSooJ1O_zr6Oj49c2JRzg&gid=0";
            UnityWebRequest www = UnityWebRequest.Get(url);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError("Download Localization ERROR " + www.error);
            }
            else
            {
                Debug.Log("Update localization done");
                string path = Application.dataPath + "/Resources/Localization/Localization.csv";
                File.WriteAllBytes(path, www.downloadHandler.data);

                AssetDatabase.Refresh();
            }
        }
        //-----------------------------------------------------------END LEGACY BLOCK
    }

    public class CoroutineClass
    {
        public void StartCoroutine(IEnumerator ienumerator)
        {
            EditorCoroutines.StartCoroutine(ienumerator, this);
        }
        public void StartCoroutine(string ienumeratorName)
        {
            EditorCoroutines.StartCoroutine(ienumeratorName, this);
        }

        public void StopCoroutine(IEnumerator ienumerator)
        {
            EditorCoroutines.StopCoroutine(ienumerator, this);
        }
        public void StopCoroutine(string ienumeratorName)
        {
            EditorCoroutines.StopCoroutine(ienumeratorName, this);
        }

        public void StopAllCoroutine()
        {
            EditorCoroutines.StopAllCoroutines(this);
        }
    }
}
