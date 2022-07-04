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
            DebugPG13.Log(new Dictionary<object, object>()
            {
                {"top left coord", polyomino.TopLeft.ToJson()},
                {"diagonal", polyomino.DiagonalVector},
                {"bottom right coord", polyomino.BottomRight.ToJson()},
                {"top left pos", topLeftPos},
                {"bottom right pos", bottomRightPos}
            });
            polyomino.transform.localPosition = (1 / 2f) * (topLeftPos + bottomRightPos);
        }

        public bool AnyGridsInBoundsAfterMove(Polyomino polyomino, Vector2Int delta)
        {
            for (var i = 0; i < polyomino.GridsCoordList.Count; i++)
            {
                if (IsGridInBoundsAfterMove(polyomino, i, delta))
                    return true;
            }
            return false;
        }

        public bool AllGridsInBounds(Polyomino polyomino)
        {
            for (var i = 0; i < polyomino.GridsCoordList.Count; i++)
            {
                if (!IsGridInBounds(polyomino, i))
                {
                    return false;
                }
            }
            return true;
        }
        
        private bool IsGridInBoundsAfterMove(Polyomino polyomino, int index, Vector2Int delta)
        {
            var coord = new Coord(polyomino.TopLeft.x, polyomino.TopLeft.y);
            coord += polyomino.GridsCoordList[index].ToVector2Int();
            coord += delta;
            
            return coord.IsInBounds(board.rows, board.cols);
        }
        
        private bool IsGridInBounds(Polyomino polyomino, int index)
        {
            var coord = polyomino.FromLocalToWorldCoord(polyomino.GridsCoordList[index]);
            return coord.IsInBounds(board.rows, board.cols);
        }
    }
   
}