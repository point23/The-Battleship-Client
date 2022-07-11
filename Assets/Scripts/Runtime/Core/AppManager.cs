using System.Linq;
using Proyecto26;
using Runtime.Common;
using Runtime.Common.Abstract;
using Runtime.Common.Responders;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Core
{
    public class AppManager : AbstractManager
    {
        public static AppManager instance;
        
        public string uriRootLocal = "http://localhost:8080/";
        public string clientVersion = "0.0.1";
        
        public DialogBuilder dialogBuilder;
        public EntranceRenderer entranceRenderer;

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

        public void Login()
        {
            _syncService.LoginService(dataSource.linksDictionary["login"]);
        }

        public void EnterGame(string uri)
        {
            GameManager.instance.EnterGame(uri);
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
            commandHub.Register("EntranceRenderer", entranceRenderer);
        }

        private void InitAppSyncService()
        {
            _syncService = new AppSyncService();
            
            _syncService.onInitApp.AddListener(commandHub.RunCommands);
            _syncService.onLogin.AddListener(commandHub.RunCommands);
            
            _syncService.InitService(uriRootLocal);
        }

        private void SetDefaultDeviceId()
        {
            RestClient.DefaultRequestParams["deviceId"] = SystemInfo.deviceUniqueIdentifier;
        }

    }
}