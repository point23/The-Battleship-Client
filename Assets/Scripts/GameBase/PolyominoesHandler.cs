using System.Collections;
using System.Collections.Generic;
using DataTypes;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace GameBase
{
    public class PolyominoesHandler : MonoBehaviour
    {
        public Board board;
        public GameObject polyominoTemplate;
        public Transform layout;

        public void Init(Board board)
        {
            this.board = board;
            layout = board.chessLayout;
        }
        
        public void GeneratePolyominoes(List<PolyominoData> dataList)
        {
            foreach (var data in dataList)
            {
                GeneratePolyomino(data);
            }
        }

        public void GeneratePolyomino(PolyominoData data)
        {
            var polyomino = Instantiate(polyominoTemplate, layout).GetComponent<Polyomino>();
            polyomino.Init(this, data);
        }
        
        public void SetPolyominoPosition(Polyomino polyomino)
        {
            var topLeftPos = board.LocalPositionOfCoord(polyomino.TopLeft);
            var bottomRightPos = board.LocalPositionOfCoord(polyomino.BottomRight);
            // DebugPG13.Log(new Dictionary<object, object>()
            // {
            //     {"top left coord", polyomino.TopLeft.ToJson()},
            //     {"diagonal", polyomino.DiagonalVector},
            //     {"bottom right coord", polyomino.BottomRight.ToJson()},
            //     {"top left pos", topLeftPos},
            //     {"bottom right pos", bottomRightPos}
            // });
            polyomino.transform.localPosition = (1 / 2f) * (topLeftPos + bottomRightPos);
        }

        public bool AnyGridsInBoundsAfterMove(Polyomino polyomino, Vector2Int delta)
        {
            foreach (var coord in polyomino.GridsCoordList)
            {
                var coordInWorldSpace = polyomino.FromLocalToWorldCoord(coord);
                coordInWorldSpace += delta;
                // DebugPG13.Log(new Dictionary<object, object>()
                // {
                //     {"coord before", (coordInWorldSpace - delta).ToJson()},
                //     {"coord after", coordInWorldSpace.ToJson()},
                //     {"is in bounds", IsGridInBounds(coordInWorldSpace)}
                // });
                if (IsGridInBounds(coordInWorldSpace))
                    return true;
            }

            return false;
        }

        public bool AllGridsInBounds(Polyomino polyomino)
        {
            foreach (var coord in polyomino.GridsCoordList)
            {
                var coordInWorldSpace = polyomino.FromLocalToWorldCoord(coord);
                if (!IsGridInBounds(coordInWorldSpace))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsGridInBounds(Coord coord)
        {
            return board.BoundingBox.IsCoordIn(coord);
        }
    }
   
}