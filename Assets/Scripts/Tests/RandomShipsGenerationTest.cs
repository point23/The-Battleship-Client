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
        public ShipsHandler ShipsHandler;
        
        public void Start()
        {
            var boundingBox = new Vector2(1, 2);
            var bottomLeft = new Vector2(0, 0);
            var grids = new List<Vector2>() { new Vector2(0,0), new Vector2(0,1)};

            var shipsDataList = new List<ShipData>();
            for (var i = 0; i < 5; i++)
            {
                shipsDataList.Add(new ShipData(boundingBox, bottomLeft, grids));
            }
            
            var ShipsDataTestPath = Application.dataPath + "/Data/Test/ships_data_test.json";
            FileHandler.SaveToJSON(shipsDataList, ShipsDataTestPath);
        }
    }
}
