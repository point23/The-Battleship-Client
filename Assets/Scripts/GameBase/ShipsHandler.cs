using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEngine;

namespace GameBase
{
    public class ShipsHandler : MonoBehaviour
    {
        public Board board;
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
            var ship  = Instantiate(shipTemplate, shipsTransform).GetComponent<Ship>();
            ship.handler = this;
            ship.Render(data);
        }

        public void SetShipPosition(Ship ship)
        {
            var topLeftPos = board.LocalPositionOfCoord(ship.TopLeft);
            var bottomRightCoord = ship.TopLeft + ship.Bounds.ToVector2() - Vector2.one;
            var bottomRightPos = board.LocalPositionOfCoord(bottomRightCoord);
            
            // DebugLogPos(ship.topLeft, bottomRightCoord, topLeftPos, bottomRightPos);
            ship.transform.localPosition = (1 / 2f) * (topLeftPos + bottomRightPos);
        }
        
        private void DebugLogPos(Coord topLeft, Coord bottomRight, Vector3 topLeftPos, Vector3 bottomRightPos)
        {
            Debug.Log("[Ship] top left coord: " + topLeft.ToJson());
            Debug.Log("[Ship] bottom right coord: " + bottomRight.ToJson());
            Debug.Log("[Ship] top left grid pos: " + topLeftPos);
            Debug.Log("[Ship] bottom right pos: " + bottomRightPos);
            Debug.Log("[Ship] local position: " + transform.localPosition);
        }
    }
   
}