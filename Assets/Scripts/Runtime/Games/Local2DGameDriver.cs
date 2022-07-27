using System;
using Cysharp.Threading.Tasks;
using Runtime.Common;
using Runtime.Common.Responders;
using Runtime.Core;
using Runtime.GameBase;
using ThirdParty.SimpleJSON;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Random = UnityEngine.Random;

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
            commandHub = GameManager.instance.commandHub;
            dataSource = GameManager.instance.dataSource;
            syncService = GameManager.instance.syncService;
        }

        public async void EnterGame()
        {
            var commands = new CommandList(_commandGenerateBoards);
            gameManager.commandHub.RunCommands(commands);

            await UniTask.DelayFrame(1);
            
            commands = new CommandList(_commandGenerateShips());
            gameManager.commandHub.RunCommands(commands);
        }

        private void StartGame()
        {
            var userFirst = Random.Range(0, 1) == 0;
            var commands = new CommandList(_commandRenderCoinToss(userFirst));
            
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