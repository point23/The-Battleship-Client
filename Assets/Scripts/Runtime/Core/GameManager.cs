using Runtime.Common.Abstract;

namespace Runtime.Core
{
    public class GameManager : AbstractManager 
    {
        public static GameManager instance;
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

        protected override void PostInit()
        {
            InitCommandHub();
            
            InitGameSyncService();
        }

        private void InitCommandHub()
        {
            
        }

        private void InitGameSyncService()
        {
            _syncService = new GameSyncService();
            
            _syncService.onEnterGame.AddListener(commandHub.RunCommands);
        }
    }
}