using System;
using System.Collections.Generic;
using System.Linq;
using DataTypes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace GameBase
{
    public class Board : MonoBehaviour
    {
        public GameObject gridTemplate;
        public GameObject polyominoesHandlerTemplate;
        public GridLayoutGroup gridsLayout;
        public Transform polyominosLayout;
        public int rows = 9;
        public int cols = 9;
        public List<Grid> grids;
        public UnityEvent<GridData> onGridClickedEvent;
        private Dictionary<Coord, List<Polyomino>> _coordPolyominosDictionary;
        [HideInInspector] public PolyominoesHandler polyominoesHandler;

        #region Properties
        
        private List<Polyomino> Polyominos => polyominoesHandler.polyominos;
        public Bounds Bounds => GetComponent<BoxCollider>().bounds;
        public BoundingBox BoundingBox => new BoundingBox(cols, rows);
        private Vector2 CellSize
        {
            get => gridsLayout.cellSize;
            set => gridsLayout.cellSize = value;
        }

        //  *________
        // |__|__|__|
        // |__|__|__|
        // |__|__|__|
        private Vector3 TopLeftPos => grids.First().transform.localPosition;
        private BoxCollider Collider => GetComponent<BoxCollider>();
        private float Height => CellSize.y * rows;
        private float Width => CellSize.x * cols;
        private Vector3 Size => new Vector3(Height, Width);

        #endregion
        
        public void Awake()
        {
            CellSize = AppManager.instance.cellSize;
            grids = new List<Grid>();
            Collider.size = Size;
            _coordPolyominosDictionary = new Dictionary<Coord, List<Polyomino>>();
            GeneratePolyominoesHandler();
        }
        
        public void GenerateGrids(int newRows=default, int newCols=default)
        {
            rows = newRows == default ? rows : newRows;
            cols = newCols == default ? cols : newCols;
            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    GenerateGrid(i, j);
                }
            }
        }

        public Vector3 LocalPositionOfCoord(Coord coord)
        {
            if (IsCoordIn(coord))
            {
                var index = ConvertCoordToIndex(coord);
                return grids[index].transform.localPosition;
            }
            
            var delta = coord.CalculateDelta(Coord.Zero);
            var displacement = new Vector2(delta.y, -delta.x) * CellSize;
            var pos = TopLeftPos + (Vector3) displacement;
            return pos;
        }

        public Coord CoordOfPosition(Vector3 position)
        {
            var localPosition = transform.InverseTransformPoint(position);
            var deltaX = localPosition.x - grids[0].LocalPosition.x;
            var deltaY = grids[0].LocalPosition.y - localPosition.y;
            var coord = new Coord(  Convert.ToInt32(deltaY / CellSize.y),  Convert.ToInt32(deltaX / CellSize.x));
            // DebugPG13.Log(new Dictionary<object, object>()
            // {
            //     {"deltaX", deltaX},
            //     {"deltaY", deltaY},
            //     {"coord", coord.ToJson()}
            // });
            return coord;
        }
        
        public void OnPolyominoRelocated(Polyomino polyomino)
        {
            RemovePolyominoFromCoordPolyominosDict(polyomino);
            AddPolyominoToCoordPolyominosDict(polyomino);
            CheckValidity();
        }

        private bool IsCoordIn(Coord coord)
        {
            return BoundingBox.IsCoordIn(coord);
        }
        
        private void CheckValidity()
        {
            foreach (var polyomino in Polyominos)
            {
                polyomino.IsGridsValid = true;
                foreach (var coord in polyomino.GridCoordsInWorldSpace)
                {
                    if (_coordPolyominosDictionary[coord].Count <= 1) continue;
                    
                    polyomino.IsGridsValid = false;
                    break;
                }
            }
        }

        private void AddPolyominoToCoordPolyominosDict(Polyomino polyomino)
        {
            foreach (var coord in polyomino.GridCoordsInWorldSpace)
            {
                if (_coordPolyominosDictionary.ContainsKey(coord))
                {
                    _coordPolyominosDictionary[coord].Add(polyomino);
                }
                else
                {
                    _coordPolyominosDictionary[coord] = new List<Polyomino> { polyomino };
                }
            }
        }

        private int ConvertCoordToIndex(Coord coord)
        {
            return BoundingBox.ConvertCoordToIndex(coord);
        }

        private void RemovePolyominoFromCoordPolyominosDict(Polyomino polyomino)
        {
            foreach (var coord in _coordPolyominosDictionary.Keys)
            {
                var polyominos = _coordPolyominosDictionary[coord];
                polyominos.Remove(polyomino);
            }
        }

        private void GenerateGrid(int i, int j)
        {
            var grid = Instantiate(gridTemplate, gridsLayout.transform).GetComponent<Grid>();
            grid.data = new GridData(i, j);
            grids.Add(grid);
        }
        
        private void GeneratePolyominoesHandler()
        {
            polyominoesHandler = Instantiate(polyominoesHandlerTemplate).GetComponent<PolyominoesHandler>();
            polyominoesHandler.Init(this);
            polyominoesHandler.onPolyominoRelocatedEvent.AddListener(OnPolyominoRelocated);
        }


        private void OnGridClicked(GridData data)
        {
            onGridClickedEvent.Invoke(data);
        }
        
    }
}
