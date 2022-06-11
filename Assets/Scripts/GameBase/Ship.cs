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

        public ShipsHandler handler;
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
            handler.SetShipPosition(this);
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
            Debug.Log("[Ship] on dragged delta:" + delta);
            // example: 
            // delta in world space (1,  1)
            // delta in coords      (-1, 0)
            data.topLeft += HandleWorldDelta(delta);
        }

        private Vector2Int HandleWorldDelta(Vector2Int delta)
        {
            return new Vector2Int(-delta.y, delta.x);
        }

        private void OnRotated()
        {
            data.Rotate();
            RenderRotation();
        }

        #endregion

    }
}