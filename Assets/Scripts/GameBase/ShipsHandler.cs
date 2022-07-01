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
        private readonly DebugFormatter _debugFormatter = new DebugFormatter("ShipsHandler");

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
            DebugLogPos(ship.TopLeft, bottomRightCoord, topLeftPos, bottomRightPos);
            ship.transform.localPosition = (1 / 2f) * (topLeftPos + bottomRightPos);
        }

        private void DebugLogPos(Coord topLeft, Coord bottomRight, Vector3 topLeftPos, Vector3 bottomRightPos)
        {
            var info = _debugFormatter.Format("SetShipPosition", "");
            info += $"top left coord: {topLeft.ToJson()}, ";
            info += $"bottom right coord: {bottomRight.ToJson()}, ";
            info += $"top left pos: {topLeftPos}, ";
            info += $"bottom right pos: {bottomRightPos}";
            info += $"new pos: {(1 / 2f) * (topLeftPos + bottomRightPos)}";
            Debug.Log(info);
        }
    }
   
}