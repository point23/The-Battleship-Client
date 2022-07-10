using Cysharp.Threading.Tasks;
using Runtime.GameBase;
using Runtime.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class TestBasicGameFlow2DLocal : MonoBehaviour
    {
        private static readonly string TestShipsJsonDataPath = Application.dataPath + "/Scripts/Test/TestData/ships_data.json";
        public Button btnStart;
        public Transform boardsTrans;
        public GameObject boardTemplate;
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
            var shipsDataList = FileHandler.ReadListFromJSON<PolyominoData>(TestShipsJsonDataPath);
            board.polyominoesHandler.GeneratePolyominoes(shipsDataList);
        }

        private async UniTask<Board> GenerateBoard()
        {
            var board = Instantiate(boardTemplate, boardsTrans).GetComponent<Board>();
            board.Init(new Vector2(100, 100));
            board.GenerateGrids(boardRows, boardCols);
            await UniTask.DelayFrame(1);
            
            return board;
        }
    }
}