using System;
using System.Collections.Generic;
using System.Linq;
using DataTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace GameBase
{
    public class Ship : MonoBehaviour
    {
        [HideInInspector] 
        public ShipData data;
        
        public GridLayoutGroup layoutGroup;
        public GameObject shipGridTemplate;

        public BoundingBox Bounds => data.bounds;
        public Coord TopLeft => data.topLeft;
        public List<Coord> Grids => data.grids;
        public int GridsCount => Bounds.height * Bounds.width;
        public List<Grid> GridList => layoutGroup.GetComponentsInChildren<Grid>().ToList();

        public void Start()
        {
            GetComponent<DragDropItem>().OnDraggedEvent.AddListener(OnDragged);
            GetComponent<MultiClickHandler>().onMultiClickedEvent.AddListener(OnRotated);
        }

        public void Render(ShipData data)
        {
            this.data = data;
            SetPosition();
            SetLayoutGroup();
            InstantiateGridObjects();
            SetGridCoords();
            RenderGrids();
        }

        private void RenderRotation()
        {
            SetLayoutGroup();
            SetPosition();
            SetGridCoords();
            RenderGrids();
        }

        private void SetPosition()
        {
            var topLeftPos = AppManager.Instance.gridsHandler.GetPosOfGrid(TopLeft);
            var bottomRightCoord = TopLeft + Bounds.ToVector2() - Vector2.one;
            var bottomRightPos = AppManager.Instance.gridsHandler.GetPosOfGrid(bottomRightCoord);
            
            // DebugLogPos(bottomRightCoord, topLeftPos, bottomRightPos);
            transform.localPosition = (1 / 2f) * (topLeftPos + bottomRightPos);
        }

        private void SetLayoutGroup()
        {
            layoutGroup.constraintCount = Bounds.width;
        }
        
        private void RenderGrids()
        {
            Debug.Log("[Ship] ship data:" + data.ToJson());
            var occupiedGrids = Grids.Select(gridCoord => gridCoord.ToIndex(Bounds.width)).ToList();
            foreach (var grid in GridList)
            {
                grid.Render(isActive: occupiedGrids.Contains(grid.Pos.ToIndex(Bounds.width)));
            }
        }

        private void InstantiateGridObjects()
        {
            for (var row = 0; row < Bounds.height; row++)
            {
                for (var col = 0; col < Bounds.width; col++)
                {
                    Instantiate(shipGridTemplate, layoutGroup.transform).GetComponent<Grid>();
                }
            }
        }
        
        private void SetGridCoords()
        {
            for (var row = 0; row < Bounds.height; row++)
            {
                for (var col = 0; col < Bounds.width; col++)
                {
                    var coord = new Coord(row, col);
                    // Debug.Log("[Ship] grid coord: " + coord.ToJson() + ", index: " + coord.ToIndex(Bounds.width));
                    GridList[coord.ToIndex(Bounds.width)].data = new GridData(coord);
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

        #region Event Listeners
        
        private void OnDragged(Vector2Int delta)
        {
            data.topLeft += delta;
        }

        private void OnRotated()
        {
            data.Rotate();
            RenderRotation();
        }

        #endregion

        #region Debug Helpers

        private void DebugLogPos(Coord bottomRight, Vector3 topLeftPos, Vector3 bottomRightPos)
        {
            Debug.Log("[Ship] top left coord: " + TopLeft.ToJson());
            Debug.Log("[Ship] bottom right coord: " + bottomRight.ToJson());
            Debug.Log("[Ship] top left grid pos: " + topLeftPos);
            Debug.Log("[Ship] bottom right pos: " + bottomRightPos);
            Debug.Log("[Ship] local position: " + transform.localPosition);
        }

        #endregion

    }
}