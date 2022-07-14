using Runtime.Common.Abstract;
using Runtime.Common.Responders;
using ThirdParty.SimpleJSON;

namespace Runtime.Core
{
    public class GameManager : CommonManager
    {
        public static GameManager instance;

        public BoardGenerator boardGenerator;
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
            commandHub.Register("CoinTossRenderer", coinTossRenderer);
        }

        private void InitGameSyncService()
        {
            syncService = new GameSyncService();
            
            syncService.onEnterGame.AddListener(commandHub.RunCommands);
        }
    }
}