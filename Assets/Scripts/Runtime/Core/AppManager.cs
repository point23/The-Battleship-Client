using Proyecto26;
using Runtime.Common;
using Runtime.Infrastructures.Helper;
using UnityEditor;
using UnityEngine;

namespace Runtime.Core
{
    public class AppManager : MonoBehaviour
    {
        public static AppManager instance;
        public Vector2 cellSize = new Vector2(100, 100);
        public string uriRootLocal = "http://localhost:8080/";
        private AppSyncService _syncService;
        
        public void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(instance);
            }
            else
            {
                instance = this;
            }

            RestClient.DefaultRequestParams["deviceId"] = SystemInfo.deviceUniqueIdentifier;
            DebugPG13.Log("default params", RestClient.DefaultRequestParams["deviceId"]);
            _syncService = new AppSyncService();
            _syncService.CreateSession(uriRootLocal + "sessions");

        }

        #region Data Paths
        
        #endregion

    }
}