using System.Collections.Generic;
using System.IO;
using DataTypes;
using GameBase;
using UnityEngine;
using Utilities;

namespace Tests
{
    /// <summary>
    /// test_local_version‘s ShipsHandler will read the ships_data.json file and generate
    /// user's ships by it and put them in random positions that is available
    /// </summary>
    public class RandomShipsGenerationTest : MonoBehaviour
    {
        public ShipsHandler shipsHandler;

        public void Start()
        {
            var ShipsDataTestPath = Application.dataPath + "/Data/Test/ships_data_test.json";
            var shipsDataList = FileHandler.ReadListFromJSON<ShipData>(ShipsDataTestPath);
            shipsHandler.GenerateShips(shipsDataList);
        }
    }
}