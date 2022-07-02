using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEngine;
using Utilities;

namespace GameBase
{
    public class ShipsHandler : MonoBehaviour
    {
        public Board board;
        public GameObject shipTemplate;
        public Transform shipsTrans;

        public void Init(Board board)
        {
            this.board = board;
            shipsTrans = board.chessTrans;
        }
        
        public void GenerateShips(List<ShipData> dataList)
        {
            foreach (var data in dataList)
            {
                GenerateShip(data);
            }
        }

        public void GenerateShip(ShipData data)
        {
            var ship  = Instantiate(shipTemplate, shipsTrans).GetComponent<Ship>();
            ship.Init(this);
            ship.Render(data);
        }
        
        public void SetShipPosition(Ship ship)
        {
            var topLeftPos = board.LocalPositionOfCoord(ship.TopLeft);
            var bottomRightCoord = ship.TopLeft + ship.Bounds.ToVector2() - Vector2.one;
            var bottomRightPos = board.LocalPositionOfCoord(bottomRightCoord);
            DebugPG13.Log(new Dictionary<object, object>()
            {
                {"top left coord", ship.TopLeft.ToJson()},
                {"bottom right coord", bottomRightCoord.ToJson()},
                {"top left pos", topLeftPos},
                {"bottom right pos", bottomRightPos}
            });
            ship.transform.localPosition = (1 / 2f) * (topLeftPos + bottomRightPos);
        }

        public bool AnyGridsInBoundsAfterMove(Ship ship, Vector2Int delta)
        {
            for (var i = 0; i < ship.GridsCoordList.Count; i++)
            {
                ship.GridsCoordList[i] += delta;
                if (ship.GridsCoordList[i].IsInBounds(board.rows, board.cols))
                {
                    ship.GridsCoordList[i] -= delta;
                    return true;
                }
                ship.GridsCoordList[i] -= delta;
            }

            return false;
        }
    }
   
}