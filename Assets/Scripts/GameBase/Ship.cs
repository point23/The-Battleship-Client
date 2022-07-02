using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
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
        public Coord TopLeft
        {
            get => data.topLeft;
            private set => data.topLeft = value;
        }

        public List<Coord> Grids => data.grids;
        public int GridsCount => Bounds.height * Bounds.width;
        public List<Grid> GridList => layoutGroup.GetComponentsInChildren<Grid>().ToList();

        public void Start()
        {
            GetComponent<DragDropItem>().onDraggedEvent.AddListener(OnDragged);
            GetComponent<MultiClickHandler>().onMultiClickedEvent.AddListener(OnRotated);
        }

        public void Init(ShipsHandler handler)
        {
            this.handler = handler;
            GetComponent<DragDropItem>().Init(new Diastimeter(handler.board));
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
                grid.Render(isActive: occupiedGrids.Contains(grid.Coord.ToIndex(Bounds.width)));
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
                    DebugPG13.Log(new Dictionary<object, object>()
                    {
                        {"grid coord", coord.ToJson()},
                        {"index", coord.ToIndex(Bounds.width)}
                    });
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
        
        private bool TryTransition(Vector2Int delta)
        {
            TopLeft += delta;
            for (var i = 0; i < Grids.Count; i++)
            {
                Grids[i] += delta;
            }

            return false;
        }

        #region Event Listeners
        private void OnDragged(Vector2Int delta)
        {
            TopLeft += delta;
            SetPosition();
        }

        private void OnRotated()
        {
            data.Rotate();
            RenderRotation();
        }

        #endregion

    }
}