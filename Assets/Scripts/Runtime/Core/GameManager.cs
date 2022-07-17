using Runtime.Common.Abstract;
using Runtime.Common.Responders;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Core
{
    public class GameManager : CommonManager
    {
        public static GameManager instance;

        [Header("Generators")]
        public BoardGenerator boardGenerator;
        public PolyominoGenerator polyominoGenerator;
        
        [Header("Renderers")]
        public CoinTossRenderer coinTossRenderer;
        
        public GameSyncService syncService;

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
            syncService.EnterGameService(uri);
        }

        public JSONNode TryGetData(string name)
        {
            return dataSource.TryGetAsset(name);
        }
        
        public T TryGetData<T>(string name) where T : new()
        {
            return dataSource.TryGetAsset<T>(name);
        }
        
        protected override void PostInit()
        {
            InitCommandHub();
            
            InitGameSyncService();
        }

        private void InitCommandHub()
        {
            commandHub.Register("BoardGenerator", boardGenerator);
            commandHub.Register("CoinTossRenderer", coinTossRenderer);
            commandHub.Register("PolyominoGenerator", polyominoGenerator);
        }

        private void InitGameSyncService()
        {
            syncService = new GameSyncService();
            
            syncService.onEnterGame.AddListener(commandHub.RunCommands);
        }
    }
}