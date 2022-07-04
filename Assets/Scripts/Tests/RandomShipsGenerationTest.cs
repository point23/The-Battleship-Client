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

        // public Button btnWriteShipData;
        public Button btnGenerate;

        public void Start()
        {
            btnGenerate.onClick.AddListener(GenerateShips);
            // WriteTestShipData();
        }

        private static void WriteTestShipData()
        {
            var shipData1 = new PolyominoData(
                new Coord(2, 2),
                new BoundingBox(2, 2),
                0,
                new List<Coord>() {new Coord(0, 0), new Coord(0, 1), new Coord(1, 1)});
            var shipData2 = new PolyominoData(
                new Coord(6, 6),
                new BoundingBox(2, 1),
                0,
                new List<Coord>() {new Coord(0, 0), new Coord(0, 1)});

            var dataList = new List<PolyominoData>();
            dataList.Add(shipData1);
            dataList.Add(shipData2);

            FileHandler.SaveToJSON(dataList, AppManager.TestShipsJsonDataPath);
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
