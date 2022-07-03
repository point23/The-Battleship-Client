using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DataTypes;
using GameBase;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Tests
{
    // test_local_version‘s ShipsHandler will read the ships_data.json file and generate
    // user's ships by it and put them in random positions that is available
    public class RandomShipsGenerationTest : MonoBehaviour
    {
        public PolyominoesHandler polyominoesHandler;
        public Transform boardsTrans;
        public GameObject boardTemplate;
        public Button btnGenerate;

        public void Start()
        {
            btnGenerate.onClick.AddListener(GenerateShips);
        }

        private async void GenerateShips()
        {
            var board = await GenerateBoard();
            polyominoesHandler.Init(board);

            var shipsDataList = FileHandler.ReadListFromJSON<PolyominoData>(AppManager.TestShipsJsonDataPath);
            polyominoesHandler.GeneratePolyominoes(shipsDataList);
            btnGenerate.gameObject.SetActive(false);
        }

        private async UniTask<Board> GenerateBoard()
        {
            var board = Instantiate(boardTemplate, boardsTrans).GetComponent<Board>();
            board.GenerateGrids(9, 9);
            await UniTask.DelayFrame(1);
            return board;
        }
    }
}
