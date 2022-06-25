using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DataTypes;
using GameBase;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Tests
{
    public class BasicGameFlowTest2D : MonoBehaviour
    {
        public Button btnStart;
        public Transform boardsTrans;
        public GameObject boardTemplate;
        public ShipsHandler shipsHandler;
        public int boardRows;
        public int boardCols;
        
        public void Start()
        {
            btnStart.onClick.AddListener(StartGame);
        }

        private async void StartGame()
        {
            var board1 = await GenerateBoard();
            var board2 = await GenerateBoard();
            GenerateShips(board1);
            btnStart.gameObject.SetActive(false);
        }

        private void GenerateShips(Board board)
        {
            var shipsDataList = FileHandler.ReadListFromJSON<ShipData>(AppManager.TestShipsJsonDataPath);
            Debug.Log(AppManager.TestShipsJsonDataPath);
            Debug.Log(shipsDataList.Count);
            shipsHandler.board = board;
            shipsHandler.GenerateShips(shipsDataList);
        }

        private async UniTask<Board> GenerateBoard()
        {
            var board = Instantiate(boardTemplate, boardsTrans).GetComponent<Board>();
            board.onGridClickedEvent.AddListener(OnGridClicked);
            board.GenerateGrids(boardRows, boardCols);
            await UniTask.DelayFrame(1);
            
            return board;
        }

        private void OnGridClicked(GridData data)
        {
            Debug.Log("[BasicGameFlowTest2D] on grid clicked" + data.ToJson());
        }
    }
}