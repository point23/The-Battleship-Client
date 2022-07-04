using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DataTypes;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace GameBase
{
    public class Polyomino : MonoBehaviour
    {
        public GridLayoutGroup layoutGroup;
        public GameObject polyominoGridTemplate;
        [HideInInspector] public PolyominoData data;
        [HideInInspector] public PolyominoesHandler handler;
        public BoundingBox Bounds => data.Bounds;

        public Coord TopLeft
        {
            get => data.topLeft;
            private set => data.topLeft = value;
        }

        public Coord BottomRight => data.topLeft + data.diagonalVector;
        public Vector2Int DiagonalVector => data.diagonalVector;

        public List<Coord> GridsCoordList => data.grids;
        public int GridsCount => Bounds.height * Bounds.width;
        public List<Grid> GridList => layoutGroup.GetComponentsInChildren<Grid>().ToList();
        public DragDropItemGroup DragDropItemGroup => GetComponent<DragDropItemGroup>();
        public MultiClickItemGroup MultiClickItemGroup => GetComponent<MultiClickItemGroup>();

        public void Start()
        {
            DragDropItemGroup.draggedEvent.AddListener(OnDragged);
            MultiClickItemGroup.multiClickedEvent.AddListener(OnRotated);
        }

        public void Init(PolyominoesHandler handler, PolyominoData data)
        {
            this.handler = handler;
            this.data = data;
            Render();
        }

        private void Render()
        {
            SetPosition();
            SetLayoutGroup();
            InstantiateGridObjects();
            InitGrids();
            RenderGrids();
            InitDragDropGroup();
            InitMultiClickItemGroup();
        }

        private void InitMultiClickItemGroup()
        {
            MultiClickItemGroup.Init(DragDropItemGroup);
        }

        private void InitDragDropGroup()
        {
            DragDropItemGroup.Init(new Diastimeter(handler.board), MultiClickItemGroup);
        }

        private void RenderRotation()
        {
            SetLayoutGroup();
            SetPosition();
            // InitGrids();
            RenderGrids();
        }

        private void SetPosition()
        {
            handler.SetPolyominoPosition(this);
        }

        private void SetLayoutGroup()
        {
            layoutGroup.constraintCount = Bounds.width;
        }

        private void InstantiateGridObjects()
        {
            for (var count = 0; count < GridsCount; count++)
            {
                Instantiate(polyominoGridTemplate, layoutGroup.transform).GetComponent<Grid>();
            }
        }

        private void InitGrids()
        {
            var occupiedGrids = GridsCoordList.Select(gridCoord => gridCoord.ToIndex(Bounds.width)).ToList();
            for (var row = 0; row < Bounds.height; row++)
            {
                for (var col = 0; col < Bounds.width; col++)
                {
                    var coord = new Coord(row, col);
                    var gridIndex = coord.ToIndex(Bounds.width);
                    var isOccupied = occupiedGrids.Contains(gridIndex);

                    GridList[gridIndex].Init(new GridData(coord, isOccupied));

                    GridList[gridIndex].GetComponent<DragDropItem>().isActive = isOccupied;
                    GridList[gridIndex].GetComponent<MultiClickItem>().isActive = isOccupied;
                    // DebugPG13.Log(new Dictionary<object, object>()
                    // {
                    //     {"grid coord", coord.ToJson()},
                    //     {"index", coord.ToIndex(Bounds.width)},
                    //     {"isOccupied", isOccupied},
                    //     {"isActive", GridList[gridIndex].GetComponent<DragDropItem>().isActive}
                    // });
                }
            }
        }

        private void ClearAllGrids()
        {
            foreach (var grid in GridList)
            {
                Destroy(grid.gameObject);
            }
        }

        private bool TryMove(Vector2Int delta)
        {
            if (!handler.AnyGridsInBoundsAfterMove(this, delta))
                return false;

            TopLeft += delta;
            return true;
        }

        private void RenderGrids()
        {
            foreach (var grid in GridList)
            {
                grid.Render();
            }
        }

        private void CheckGridsValidity()
        {
            foreach (var grid in GridList)
                grid.IsValid = AllGridsInBounds;
        }

        private bool AllGridsInBounds => handler.AllGridsInBounds(this);

        public Coord FromLocalToWorldCoord(Coord localCoord)
        {
            var coord = new Coord(TopLeft);
            var angleRotated = Vector3.Angle(new Vector3(1, 1, 0), data.DiagonalVector3);
            var delta = Quaternion.Euler(0, 0, -angleRotated) * localCoord.ToVector2();
            coord += delta;
            return coord;
        }

        private void RotateClockwiseAround(Vector2 centerPoint)
        {
            data.RotateClockwiseAround(centerPoint);
            transform.Rotate(0,0,-90);
        }

        #region Event Listeners

        private void OnDragged(Vector2Int delta)
        {
            if (TryMove(delta))
            {
                CheckGridsValidity();
                RenderGrids();
            }
            SetPosition();
        }

        private void OnRotated(MultiClickItem item)
        {
            DebugPG13.Log(new Dictionary<object, object>()
            {
                {"center coord before", item.GetComponent<Grid>().Coord.ToJson()},
                {"center before", FromLocalToWorldCoord(item.GetComponent<Grid>().Coord).ToVector2()},
                {"diagonal before", DiagonalVector}
            });
            RotateClockwiseAround(FromLocalToWorldCoord(item.GetComponent<Grid>().Coord).ToVector2());
            DebugPG13.Log(new Dictionary<object, object>()
            {
                {"diagonal after", DiagonalVector}
            });
            CheckGridsValidity();
            RenderRotation();
            DebugPG13.Log("Ship Data", data.ToJson());
        }
        
        #endregion

    }
}