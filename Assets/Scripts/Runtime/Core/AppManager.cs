using Proyecto26;
using Runtime.Common.Abstract;
using Runtime.Common.Responders;
using UnityEngine;

namespace Runtime.Core
{
    public class AppManager : CommonManager
    {
        public static AppManager instance;
        
        public Transform gameCameraPos;
        public Transform uiCameraPos;
        
        public string uriRootLocal = "http://localhost:8080/";
        public string clientVersion = "0.0.1";
        
        public DialogBuilder dialogBuilder;
        public EntranceGenerator entranceGenerator;

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
            SetPosOfMainCamera(gameCameraPos.position);
            
            GameManager.instance.EnterGame(uri);
        }

        protected override void PostInit()
        {
            SetPosOfMainCamera(uiCameraPos.position);
            
            SetDefaultDeviceId();
            
            InitCommandHub();
            
            InitAppSyncService();
        }

        private void InitCommandHub()
        {
            commandHub.Register("DataSource", dataSource);
            commandHub.Register("DialogBuilder", dialogBuilder);
            commandHub.Register("EntranceGenerator", entranceGenerator);
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

        private void SetPosOfMainCamera(Vector3 position)
        {
            Camera.main.transform.position = position;
        }
    }
}