﻿using Runtime.Common;
using Runtime.Common.Responders;
using Runtime.Core;
using Runtime.GameBase;
using Runtime.Infrastructures.Helper;
using ThirdParty.SimpleJSON;
using UnityEngine;

namespace Runtime.Games
{
    public class Local2DGameDriver : MonoBehaviour
    {
        public GameManager gameManager;
        public CommandHub commandHub;
        public DataSource dataSource;
        public GameSyncService syncService;

        public void Awake()
        {
            commandHub = GameManager.instance.commandHub;
            dataSource = GameManager.instance.dataSource;
            syncService = GameManager.instance.syncService;
        }

        public void EnterGame()
        {
            var boardDataUser = BoardData.DefaultJsonData;
            var boardDataEnemy = BoardData.DefaultJsonData;
            boardDataUser["id"] = "user";
            boardDataEnemy["id"] = "enemy";
            var boardsData = new JSONArray();
            boardsData.Add(boardDataUser);
            boardsData.Add(boardDataEnemy);

            var commandsGenerateBoards = new Command("BoardGenerator", "Generate", boardsData);
            var commands = new CommandList(commandsGenerateBoards);

            var coinTossData = JSONNode.Parse("{\"result\":\"head\"}");
            commands.Attach(new Command("CoinTossRenderer", "Toss", coinTossData));
            
            syncService.onEnterGame.Invoke(commands);
        }
    }
}