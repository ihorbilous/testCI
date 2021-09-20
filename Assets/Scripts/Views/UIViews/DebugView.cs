using Consolation;
using PFS.Assets.Scripts.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PFS.Assets.Scripts.Views.DebugScreen
{
    public class DebugView : BaseView
    {
      

        [Header("Switchers")]
        //---SHOW/HIDE CONSOLE----------
        public Button showHideConsole;
        public Slider showHideConsoleSlider;
        private ConsoleLog debugConsole;
        private bool isConsoleShowed = false;
        //-----------------------------

      

       

        //---BUILD VERSION-------------
        public TextMeshProUGUI buildVersion;
        public string version;
        //-----------------------------

      


        public void LoadView()
        {

            //---BUILD VERSION-------------
            SetBuildVersion();
            //-----------------------------

        }

        public void RemoveView()
        {

        }

      

      

     

       

        #region ---BUID VERSION--------
        private void SetBuildVersion()
        {
            buildVersion.text = version;
        }
        #endregion

       
    }
}