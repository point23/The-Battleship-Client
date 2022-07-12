using Cysharp.Threading.Tasks.Triggers;
using Runtime.Common.Abstract;
using Runtime.Common.Responders;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Core
{
    public class GameManager : CommonManager
    {
        public static GameManager instance;

        public BoardGenerator boardGenerator;
        
        private GameSyncService _syncService;

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
        
        public void EnterGame(string uri)
        {
            _syncService.EnterGameService(uri);
        }

        public JSONNode TryGetData(string name)
        {
            return dataSource.TryGetData(name);
        }
        
        public T TryGetData<T>(string name) where T : new()
        {
            return dataSource.TryGetData<T>(name);
        }
        
        protected override void PostInit()
        {
            InitCommandHub();
            
            InitGameSyncService();
        }

        private void InitCommandHub()
        {
            commandHub.Register("BoardGenerator", boardGenerator);
            DebugPG13.Log("contains BoardGenerator", commandHub.ContainsResponder("BoardGenerator"));
        }

        private void InitGameSyncService()
        {
            _syncService = new GameSyncService();
            
            _syncService.onEnterGame.AddListener(commandHub.RunCommands);
        }
    }
}