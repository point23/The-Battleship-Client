using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEngine;

namespace GameBase
{
    public class ShipsHandler : MonoBehaviour
    {
        public GameObject shipTemplate;
        public Transform shipsTransform;

        public void GenerateShips(List<ShipData> dataList)
        {
            foreach (var data in dataList)
            {
                GenerateShip(data);
            }
        }

        public void GenerateShip(ShipData data)
        {
            var go = Instantiate(shipTemplate, shipsTransform);
            go.GetComponent<Ship>().Render(data);
        }
    }
   
}