using DataTypes;
using GameBase;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Tests
{
    // test_local_version‘s ShipsHandler will read the ships_data.json file and generate
    // user's ships by it and put them in random positions that is available
    public class RandomShipsGenerationTest : MonoBehaviour
    {
        public ShipsHandler shipsHandler;
        public Button btnGenerateShip;

        public void Start()
        {
            btnGenerateShip.onClick.AddListener(GenerateShip);
            
            // var shipData = new ShipData(
            //         new BoundingBox(2, 2), 
            //         new Coord(2, 2),
            //         new List<Coord>() {new Coord(0, 0), new Coord(0, 1), new Coord(1, 1)});
            // var dataList = new List<ShipData>();
            // for (var i = 0; i < 2; i++)
            // {
            //     dataList.Add(shipData);
            // }
            // FileHandler.SaveToJSON(dataList, _shipsDataTestPath);
        }

        private void GenerateShip()
        {
            var shipsDataList = FileHandler.ReadListFromJSON<ShipData>(AppManager.Instance.testShipsJsonDataPath);
            shipsHandler.GenerateShips(shipsDataList);
        }
    }
}
