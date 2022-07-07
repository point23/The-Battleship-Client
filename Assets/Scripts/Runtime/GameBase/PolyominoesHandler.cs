using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GameBase
{
    public class PolyominoesHandler : MonoBehaviour
    {
        public Board board;
        public GameObject polyominoTemplate;
        public List<Polyomino> polyominos;
        [HideInInspector] public UnityEvent<Polyomino> onPolyominoRelocatedEvent;
        private Transform Layout => board.polyominosLayout;

        public void Init(Board newBoard)
        {
            board = newBoard;
        }

        public void GeneratePolyominoes(List<PolyominoData> dataList)
        {
            foreach (var data in dataList)
            {
                polyominos.Add(GeneratePolyomino(data));
            }
        }

        public void RelocatePolyomino(Polyomino polyomino)
        {
            var topLeftPos = board.LocalPositionOfCoord(polyomino.TopLeft);
            var bottomRightPos = board.LocalPositionOfCoord(polyomino.BottomRight);
            polyomino.transform.localPosition = (1 / 2f) * (topLeftPos + bottomRightPos);
            // DebugPG13.Log(new Dictionary<object, object>()
            // {
            //     {"top left coord", polyomino.TopLeft.ToJson()},
            //     {"diagonal", polyomino.DiagonalVector},
            //     {"bottom right coord", polyomino.BottomRight.ToJson()},
            //     {"top left pos", topLeftPos},
            //     {"bottom right pos", bottomRightPos}
            // });
        }

        public void OnPolyominoRelocated(Polyomino polyomino)
        {
            RelocatePolyomino(polyomino);
            onPolyominoRelocatedEvent.Invoke(polyomino);
            RenderPolyominos();
        }

        private void RenderPolyominos()
        {
            polyominos.ForEach(p => p.RenderGrids());
        }

        public bool AnyGridsInBoundsAfterMove(Polyomino polyomino, Vector2Int delta)
        {
            foreach (var coord in polyomino.GridCoordsInWorldSpace)
            {
                var temp = new Coord(coord);
                temp += delta;
                if (IsCoordInBounds(temp))
                    return true;
            }

            return false;
        }

        public bool AllGridsInBounds(Polyomino polyomino)
        {
            return polyomino.GridCoordsInWorldSpace.All(IsCoordInBounds);
        }

        private bool IsCoordInBounds(Coord coord)
        {
            return board.BoundingBox.IsCoordIn(coord);
        }
        
        private Polyomino GeneratePolyomino(PolyominoData data)
        {
            var polyomino = Instantiate(polyominoTemplate, Layout).GetComponent<Polyomino>();
            polyomino.relocatedEvent.AddListener(OnPolyominoRelocated);
            polyomino.Init(this, data);
            return polyomino;
        }
    }
   
}