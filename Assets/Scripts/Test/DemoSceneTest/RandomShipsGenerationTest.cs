using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Runtime.GameBase;
using Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class RandomShipsGenerationTest : MonoBehaviour
    {
        private static readonly string TestShipsJsonDataPath = Application.dataPath + "/Scripts/Test/TestData/ships_data.json";
        public Transform boardsTrans;
        public GameObject boardTemplate;
        public Button btnWriteShipData;
        public Button btnGenerate;
        [Header("ShipDataList")]
        [SerializeField] public List<PolyominoData> testShipDataList;

        public void Start()
        {
            btnGenerate.onClick.AddListener(GenerateShips);
            btnWriteShipData.onClick.AddListener(WriteTestShipData);
        }

        private void WriteTestShipData()
        {
            if (testShipDataList.Count == 0)
            {
                return;
            }
            FileHandler.SaveToJSON(testShipDataList, TestShipsJsonDataPath);
        }

        private async void GenerateShips()
        {
            var board = await GenerateBoard();
            var shipsDataList = FileHandler.ReadListFromJSON<PolyominoData>(TestShipsJsonDataPath);
            board.polyominoesHandler.GeneratePolyominoes(shipsDataList);
            btnGenerate.gameObject.SetActive(false);
            btnWriteShipData.gameObject.SetActive(false);
        }

        private async UniTask<Board> GenerateBoard()
        {
            var board = Instantiate(boardTemplate, boardsTrans).GetComponent<Board>();
            board.Init(new Vector2(100, 100));
            board.GenerateGrids(9, 9);
            await UniTask.DelayFrame(1);
            return board;
        }
    }
}
