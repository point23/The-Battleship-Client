using Runtime.Common;
using Runtime.Common.Responders;
using Runtime.Core;
using Runtime.GameBase;
using ThirdParty.SimpleJSON;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Runtime.Games
{
    public class Local2DGameDriver : MonoBehaviour
    {
        public GameManager gameManager;
        public CommandHub commandHub;
        public DataSource dataSource;
        public GameSyncService syncService;
        private static readonly string FleetDataPath = Application.dataPath + "/Resources/Data/fleets_data.json";

        public void Awake()
        {
            Debug.Log(FleetDataPath);
            commandHub = GameManager.instance.commandHub;
            dataSource = GameManager.instance.dataSource;
            syncService = GameManager.instance.syncService;
        }

        public void EnterGame()
        {
            var userFirst = Random.Range(0, 1) == 0;
            
            var commands = new CommandList(_commandGenerateBoards);
            commands.Add(_commandGenerateShips());
            
            // commands.Add(_commandRenderCoinToss(userFirst));
            
            syncService.onEnterGame.Invoke(commands);
        }
        
        #region Static Commands

        private readonly Command _commandGenerateBoards =  new("BoardGenerator", "Generate", new JSONArray
        {
            BoardData.DefaultJsonDataWithId("user"),
            BoardData.DefaultJsonDataWithId("enemy")
        });
        
        private static Command _commandGenerateShips()
        {
            var file = Resources.Load("Data/fleets_data") as TextAsset;
            Debug.Log(file!.text);
            return new Command("PolyominoGenerator", "Generate", JSONNode.Parse(file!.text));
        }
        private static Command _commandRenderCoinToss(bool userFirst)
        {
            var coinTossData = JSONNode.Parse("{\"result\":\"\"}");
            coinTossData["result"].Value = userFirst ? "head" : "tail";
            return new Command("CoinTossRenderer", "Toss", coinTossData);
        }

        #endregion
    }
}