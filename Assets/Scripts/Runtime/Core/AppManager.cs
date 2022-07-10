using Proyecto26;
using Runtime.Common;
using Runtime.Common.Abstract;
using Runtime.Common.Responders;
using Runtime.Infrastructures.Helper;
using Runtime.Infrastructures.JSON;
using UnityEditor;
using UnityEngine;

namespace Runtime.Core
{
    public class AppManager : AbstractManager
    {
        public static AppManager instance;
        public string uriRootLocal = "http://localhost:8080/";
        public DialogBuilder dialogBuilder;
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

            Init();
        }
        
        protected override void PostInit()
        {
            SetDefaultDeviceId();
            
            InitCommandHub();
            
            InitAppSyncService();
        }

        private void InitCommandHub()
        {
            commandHub.Register("DataSource", dataSource);
            commandHub.Register("DialogBuilder", dialogBuilder);
        }

        private void InitAppSyncService()
        {
            _syncService = new AppSyncService();
            
            _syncService.onInit.AddListener(commandHub.RunCommands);
            _syncService.onLogin.AddListener(commandHub.RunCommands);
            
            _syncService.InitService(uriRootLocal);
        }

        private void SetDefaultDeviceId()
        {
            RestClient.DefaultRequestParams["deviceId"] = SystemInfo.deviceUniqueIdentifier;
        }

    }
}